using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Backend.Services
{
    public class CachingService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly VyService _vyService;
        private readonly ILogger _logger;

        public CachingService(VyService vyService, ILogger<CachingService> logger)
        {
            _vyService = vyService;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(UpdateCache, null, TimeSpan.Zero, TimeSpan.FromHours(2));

            return Task.CompletedTask;
        }
        private async void UpdateCache(object state)
        {
            // TODO: Get prices for three months in the future
            string[] citiesToCache = { "Bergen", "Stavanger", "Trondheim", "Kristiansand", "Ålesund" };
            var nextMonth = DateTime.Today.AddMonths(1);
            var twoMonthsFromNow = DateTime.Today.AddMonths(2);
            DateTime[] dates = { DateTime.Today, new DateTime(nextMonth.Year, nextMonth.Month, 1), new DateTime(twoMonthsFromNow.Year, twoMonthsFromNow.Month, 1) };
            foreach (string city in citiesToCache)
            {
                foreach (DateTime date in dates)
                {
                    try
                    {
                        await _vyService.UpdatePricesAsync(date, "Oslo S", city);
                        await _vyService.UpdatePricesAsync(date, city, "Oslo S");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($@"Error updating cache for cities {city} and Oslo S for date {date}
                            Got Exception {ex}");
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}