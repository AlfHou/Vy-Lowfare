using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Backend.Services
{
    public class CachingService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly VyService _vyService;

        public CachingService(VyService vyService)
        {
            _vyService = vyService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(UpdateCache, null, TimeSpan.Zero, TimeSpan.FromHours(2));

            return Task.CompletedTask;
        }
        private void UpdateCache(object state)
        {
            // TODO: Get prices for three months in the future
            string[] citiesToCache = {"Bergen", "Stavanger", "Trondheim", "Kristiansand", "Ålesund"};
            foreach (string city in  citiesToCache) {
                _vyService.UpdateLowestPriceDayCacheAsync(DateTime.Today, city, "Oslo S");
                _vyService.UpdateLowestPriceDayCacheAsync(DateTime.Today, "Oslo S", city);
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