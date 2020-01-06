using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Models
{
    public class ItinerariesResponse : IComparable<ItinerariesResponse>
    {
        public DateTime DepartureScheduled { get; set; }
        public IList<PriceOption> PriceOptions { get; set; }
        public bool NightTrain { get; set; }

        // Returns the lowest price of the price options that is not 0,
        // or returns 0 if all prices are 0.
        // Phoo! That's a lot of zeros :P
        public int LowestPrice(){
            return this.PriceOptions
            .Select(priceOption => priceOption.Amount)
            .Where(price =>
            {
                return price != 0;
            }).Min();
        }
        public int CompareTo(ItinerariesResponse obj)
        {
            var lowestPriceSelf = this.LowestPrice();

            var lowestPriceOther = obj.LowestPrice();

            if (lowestPriceOther == 0 && lowestPriceSelf == 0)
            {
                return 0;
            }
            if (lowestPriceSelf == 0)
            {
                return 1;
            }
            if (lowestPriceOther == 0)
            {
                return -1;
            }
            if (lowestPriceOther == lowestPriceSelf)
            {
                if ((this.NightTrain && obj.NightTrain) || !(this.NightTrain && obj.NightTrain))
                {
                    return 0;
                }
                if (this.NightTrain && !obj.NightTrain)
                {
                    return 1;
                }
                return -1;
            }
            return lowestPriceSelf.CompareTo(lowestPriceOther);
        }
    }
}