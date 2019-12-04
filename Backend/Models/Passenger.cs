using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Backend.Models
{
    public class Passenger : IEquatable<Passenger>
    {
        public String Type { get; set; } = "ADULT";
        public String CustomerNumber { get; set; } = (String)null;
        public String Discount { get; set; } = "NONE";
        public List<int> Extras { get; set; } = new List<int>(0);

        public bool Equals([AllowNull] Passenger other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (Type.CompareTo(other.Type) == 0)
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Passenger);
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }
    }
}