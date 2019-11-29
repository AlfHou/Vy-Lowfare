using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public IEnumerable<int> Get(DateTime date)
        {
            System.Console.WriteLine(date);
            return _vyService.GetPrices(date);
        }
    }
}
