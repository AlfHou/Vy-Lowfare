using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Backend.Services
{
    public class VyService
    {
        private static HttpClient _vyClient;
        private readonly IMemoryCache _cache;
        private readonly ILogger _logger;
        public VyService(IHttpClientFactory clientFactory, IMemoryCache cache, ILogger<VyService> logger)
        {
            _vyClient = clientFactory.CreateClient("vyClient");
            _cache = cache;
            _logger = logger;
        }
        public async Task UpdateLowestPriceDayCacheAsync(DateTime date, String to, String from)
        {
            var endpoint = "api/itineraries/search";
            var values = new VyQuery
            {
                To = to,
                From = from,
                Time = date.ToString("yyyy-MM-ddTHH:mm"),
            };

            using (var cacheEntry = _cache.CreateEntry(values))
            {
                cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(3);
                _logger.LogInformation($"Updating cache entry for destination: {to}, from: {from}, for date {date}");

                var serializeSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                var valuesSerialized = JsonConvert.SerializeObject(values, serializeSettings);
                var content = new StringContent(valuesSerialized, Encoding.UTF8, "application/json");
                var response = await _vyClient.PostAsync(endpoint, content);
                _logger.LogInformation($"Received response from Vy for date {date} with status code: {response.StatusCode}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorString = await response.Content.ReadAsStringAsync();

                    _logger.LogError($@"Failed to get itineraries from: {from}, to: {to} for date {date}.
                            Received error code: {response.StatusCode} Message: {errorString}");
                    throw new Exception($"Failed to get itinerary information for {date}");
                }
                var responseString = await response.Content.ReadAsStringAsync();
                var definition = new
                {
                    Itineraries = new List<ItinerariesResponse>()
                };

                var deserializeSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,

                };

                try
                {
                    var responseList = JsonConvert.DeserializeAnonymousType(responseString, definition, deserializeSettings);

                    
                    var lowestItineraryResponse = responseList.Itineraries.Min();
                    var lowestOption = new LowPriceOption{
                        Amount = lowestItineraryResponse.LowestPrice(),
                        NightTrain = lowestItineraryResponse.NightTrain
                    };

                    cacheEntry.Value = lowestOption;
                }
                catch (Exception)
                {
                    _logger.LogInformation($"Failed to get price for date {date}. Returning 0");
                    cacheEntry.Value = null;
                }
            };
        }
        private async Task<LowPriceOption> GetLowestPriceDayAsync(DateTime date, String to, String from)
        {
            var endpoint = "api/itineraries/search";
            var values = new VyQuery
            {
                To = to,
                From = from,
                Time = date.ToString("yyyy-MM-ddTHH:mm"),
            };
            var cacheEntry = await _cache.GetOrCreateAsync<LowPriceOption>(values, async entry =>
            {
                _logger.LogInformation($"Entry for date {date} not in cache. Fetching");
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(3);

                var serializeSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                var valuesSerialized = JsonConvert.SerializeObject(values, serializeSettings);
                var content = new StringContent(valuesSerialized, Encoding.UTF8, "application/json");
                var response = await _vyClient.PostAsync(endpoint, content);
                _logger.LogInformation($"Received response from Vy for date {date} with status code: {response.StatusCode}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorString = await response.Content.ReadAsStringAsync();

                    _logger.LogError($@"Failed to get itineraries from: {from}, to: {to} for date {date}.
                        Received error code: {response.StatusCode} Message: {errorString}");
                    throw new Exception($"Failed to get itinerary information for {date}");
                }
                var responseString = await response.Content.ReadAsStringAsync();
                var definition = new
                {
                    Itineraries = new List<ItinerariesResponse>()
                };

                var deserializeSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,

                };

                try
                {
                    var responseList = JsonConvert.DeserializeAnonymousType(responseString, definition, deserializeSettings);

                    
                    var lowestItineraryResponse = responseList.Itineraries.Min();
                    var lowestOption = new LowPriceOption{
                        Amount = lowestItineraryResponse.LowestPrice(),
                        NightTrain = lowestItineraryResponse.NightTrain
                    };

                    return lowestOption;
                }
                catch (Exception)
                {
                    _logger.LogInformation($"Failed to get price for date {date}. Returning 0");
                    return new LowPriceOption {
                        Amount = 0,
                        NightTrain = false
                    };
                }
            });
            return cacheEntry;
        }
        public async Task UpdatePricesAsync(DateTime date, String to, String from)
        {
            var dateCounter = date;
            while (dateCounter.Month == date.Month)
            {
                await UpdateLowestPriceDayCacheAsync(dateCounter, to, from);
                dateCounter = dateCounter.AddDays(1);
                // Sleep 0.5 seconds to not spam Vy's servers when all I am doing is caching
                Thread.Sleep(500);
            }
        }

        // Get all prices for the month of 'date'
        public IEnumerable<LowPriceOption> GetPricesAsync(DateTime date, String to, String from)
        {
            _logger.LogInformation($"Getting prices from: {from}, to: {to}, from date: {date}");
            // Space for atleast 31 days
            var priceList = new List<Task<LowPriceOption>>(31);
            var dateCounter = date;
            while (dateCounter.Month == date.Month)
            {
                priceList.Add(GetLowestPriceDayAsync(dateCounter, to, from));
                dateCounter = dateCounter.AddDays(1);
            }

            try
            {
                return Task.WhenAll(priceList).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
