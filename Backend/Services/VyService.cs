using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Backend.Services
{
    public class VyService
    {
        private static HttpClient _vyClient;
        private readonly IMemoryCache _cache;
        public VyService(IHttpClientFactory clientFactory, IMemoryCache cache)
        {
            _vyClient = clientFactory.CreateClient("vyClient");
            _cache = cache;
        }
        private async Task<int> GetLowestPriceDayAsync(DateTime date)
        {
            var endpoint = "api/itineraries/search";
            var values = new
            {
                to = "Bergen",
                from = "Oslo S",
                time = date.ToString("yyyy-MM-ddTHH:mm"),
                limitResultsToSameDay = "true",
                language = "no",
                passengers = new List<object>() {
                    new {
                        type = "ADULT",
                        customerNumber = (string)null,
                        discount = "NONE",
                        extras = new List<int>(0)
                    }
                },
                priceNecessity = "REQUIRED",
                hasReturnTrip = false
            };
            // TODO: Fix cache key. It needs to be more unique. Maybe the hash of 'values'?
            var cacheEntry = await _cache.GetOrCreateAsync("test", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(3);
                entry.SlidingExpiration = TimeSpan.FromHours(1);

                var valuesSerialized = JsonConvert.SerializeObject(values);
                var content = new StringContent(valuesSerialized, Encoding.UTF8, "application/json");
                var response = await _vyClient.PostAsync(endpoint, content);
                var responseString = await response.Content.ReadAsStringAsync();
                var definition = new
                {
                    Itineraries = new List<ItinerariesResponse>()
                };

                var deserializeSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
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
        public IEnumerable<int> GetPricesAsync(DateTime date)
        {
            // Space for atleast 31 days
            var priceList = new List<Task<int>>(31);
            var dateCounter = date;
            while (dateCounter.Month == date.Month)
            {
                priceList.Add(GetLowestPriceDayAsync(dateCounter));
                dateCounter = dateCounter.AddDays(1);
            }

            return Task.WhenAll(priceList).Result;
        }
    }
}
