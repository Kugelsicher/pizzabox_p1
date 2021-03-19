using System;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
    /// <summary>
    /// Referenced <https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/how-to-define-value-equality-for-a-type>
    /// while implementing the methods of IEquatable
    /// </summary>
    public class Crust : AComponent, IEquatable<Crust>
    {
        private Crust() {} //Required for XmlSerializer()
        public Crust(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public bool Equals(Crust other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.GetType() != other.GetType())
            {
                return false;
            }

            return Name == other.Name;
        }

        public override bool Equals(object other)
        {
            return this.Equals(other as Crust);
        }

        public static bool operator ==(Crust lhs, Crust rhs)
        {
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    return true;
                }

                return false;
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Crust lhs, Crust rhs)
        {
            return !(lhs == rhs);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}