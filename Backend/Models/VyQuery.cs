using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public class VyQuery
    {

        public String To { get; set; }
        public String From { get; set; }
        public String Time { get; set; }
        public String LimitResultsToSameDay { get; set; } = "true";
        public String Language { get; set; } = "no";
        public List<Passenger> Passengers { get; set; } = new List<Passenger> { new Passenger() };
        public String PriceNecessity { get; set; } = "REQUIRED";
        public Boolean HasReturnTrip { get; set; } = false;
    }

}