using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Backend.Models
{
    public class VyQuery : IEquatable<VyQuery>
    {

        public String To { get; set; }
        public String From { get; set; }
        public String Time { get; set; }
        public String LimitResultsToSameDay { get; set; } = "true";
        public String Language { get; set; } = "no";
        public List<Passenger> Passengers { get; set; } = new List<Passenger> { new Passenger() };
        public String PriceNecessity { get; set; } = "REQUIRED";
        public Boolean HasReturnTrip { get; set; } = false;


        // Needed for IMemoryCache equality check
        public bool Equals([AllowNull] VyQuery other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            var toEqual = To.CompareTo(other.To);
            if (toEqual != 0)
            {
                return false;
            }

            var fromEqual = From.CompareTo(other.From);
            if (fromEqual != 0)
            {
                return false;
            }

            // Don't compare time of day.
            var queryTime = DateTime.Parse(Time);
            var queryTimeDay = new DateTime(queryTime.Year, queryTime.Month, queryTime.Day);

            var compareToTime = DateTime.Parse(other.Time);
            var compareToTimeDay = new DateTime(compareToTime.Year, compareToTime.Month, compareToTime.Day);

            var timeEqual = queryTimeDay.CompareTo(compareToTimeDay);
            if (timeEqual != 0)
            {
                return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as VyQuery);
        }

        public override int GetHashCode()
        {
            var toHashCode = To.GetHashCode();
            var fromHashCode = From.GetHashCode();

            var queryTime = DateTime.Parse(Time);
            var queryTimeDay = new DateTime(queryTime.Year, queryTime.Month, queryTime.Day);
            var timeHash = queryTimeDay.GetHashCode();

            return ((toHashCode + fromHashCode + timeHash) / 3);
        }

    }

}