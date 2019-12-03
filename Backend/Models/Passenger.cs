using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public class Passenger
    {
        public String Type { get; set; } = "ADULT";
        public String CustomerNumber { get; set; } = (String)null;
        public String Discount { get; set; } = "NONE";
        public List<int> Extras { get; set; } = new List<int>(0);

    }
}