using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JourneyController : ControllerBase
    {
        private readonly VyService _vyService;

        public JourneyController(VyService vyService)
        {
            _vyService = vyService;
        }
        [HttpGet]
        public IEnumerable<int> Get(DateTime date, String to, String from)
        {
            var thisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            // Earlier than current month
            if (DateTime.Compare(new DateTime(date.Year, date.Month, 1), thisMonth) < 0)
            {
                return new List<int>(0);
            }
            // Current month. Find prices from today
            else if (DateTime.Compare(new DateTime(date.Year, date.Month, 1), thisMonth) == 0)
            {
                var queryDateFrom = DateTime.Today.AddMinutes(1);
                var response = _vyService.GetPricesAsync(queryDateFrom, to, from);
                return response;

            }
            else
            {
                var queryDateFrom = new DateTime(date.Year, date.Month, 1).AddMinutes(1);
                var response = _vyService.GetPricesAsync(queryDateFrom, to, from);
                return response;

            }
        }
    }
}
