using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class JourneyController : ControllerBase
    {
        private readonly VyService _vyService;
        private readonly ILogger _logger;

        public JourneyController(VyService vyService, ILogger<JourneyController> logger)
        {
            _vyService = vyService;
            _logger = logger;
        }
        [HttpGet]
        public ActionResult<IEnumerable<LowPriceOption>> Get(DateTime? date, string to, string from)
        {
            if (String.IsNullOrEmpty(to)) {
                return BadRequest("Destination must be specified");
            }
            if (String.IsNullOrEmpty(from)) {
                return BadRequest("Departure location must be specified");
            }
            if (!date.HasValue) {
                return BadRequest("Please specify a date");
            }

            var thisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            // Earlier than current month
            if (DateTime.Compare(new DateTime(date.Value.Year, date.Value.Month, 1), thisMonth) < 0)
            {
                _logger.LogInformation($"Requested date earlier than this month. Requested: {date}. Current: {DateTime.Today}");
                return new List<LowPriceOption>(0);
            }
            // Current month. Find prices from today
            else if (DateTime.Compare(new DateTime(date.Value.Year, date.Value.Month, 1), thisMonth) == 0)
            {
                _logger.LogInformation($"Requested date is this month. Requested: {date}. Current: {DateTime.Today}");
                var queryDateFrom = DateTime.Today.AddMinutes(1);
                try
                {
                    var response = _vyService.GetPricesAsync(queryDateFrom, to, from);
                    var responseList = new List<LowPriceOption>(response);
                    _logger.LogInformation($"Response is {responseList.Count} entries long");
                    return responseList;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"Bad Request, got exception: {ex}");
                    return BadRequest("Something went wrong");
                }

            }
            else
            {
                _logger.LogInformation($"Requested date is in a future month. Requested: {date}. Current: {DateTime.Today}");
                var queryDateFrom = new DateTime(date.Value.Year, date.Value.Month, 1).AddMinutes(1);
                try
                {
                    var response = _vyService.GetPricesAsync(queryDateFrom, to, from);
                    var responseList = new List<LowPriceOption>(response);
                    _logger.LogInformation($"Response is {responseList.Count} entries long");
                    return responseList;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"Bad Request, got exception: {ex}");
                    return BadRequest("Something went wrong");
                }

            }
        }
    }
}
