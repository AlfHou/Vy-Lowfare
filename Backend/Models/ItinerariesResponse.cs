using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public class ItinerariesResponse
    {
        public DateTime DepartureScheduled { get; set; }
        public IList<PriceOption> PriceOptions { get; set; }
    }
}