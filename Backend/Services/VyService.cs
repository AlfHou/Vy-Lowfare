using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        private async Task<int> GetLowestPriceDayAsync(DateTime date, String to, String from)
        {
            var endpoint = "api/itineraries/search";
            var values = new VyQuery
            {
                To = to,
                From = from,
                Time = date.ToString("yyyy-MM-ddTHH:mm"),
            };
            var cacheEntry = await _cache.GetOrCreateAsync(values, async entry =>
            {
                _logger.LogInformation($"Entry for date {date} not in cache. Fetching");
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4);
                entry.SlidingExpiration = TimeSpan.FromHours(1);

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

                var responseList = JsonConvert.DeserializeAnonymousType(responseString, definition, deserializeSettings);
                var lowestPrice = responseList.Itineraries.SelectMany(response =>
                {
                    return response.PriceOptions
                        .Select(priceOption =>
                        {
                            return priceOption.Amount;
                        }).Where(value =>
                        {
                            return value != 0;

                        });

                }).Min();
                return lowestPrice;
            });
            return cacheEntry;
        }
        public IEnumerable<int> GetPricesAsync(DateTime date, String to, String from)
        {
            _logger.LogInformation($"Getting prices from: {from}, to: {to}, from date: {date}");
            // Space for atleast 31 days
            var priceList = new List<Task<int>>(31);
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
