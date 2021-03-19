using System;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Singletons;
using PizzaBox.Storing;

namespace PizzaBox.Domain.Models
{
    public class CustomPizza : APizza, IEquatable<CustomPizza>
    {
        /// <summary>
        /// Referenced <https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/how-to-define-value-equality-for-a-type>
        /// while implementing the methods of IEquatable
        /// </summary>
        //private CustomPizza() {} //Required for XmlSerializer()

        public CustomPizza()
        {
            Name = "Custom Pizza";
            Crust = ComponentSingleton.Instance.Crusts.Find(c => "Hand Tossed" == c.Name);
            Size = ComponentSingleton.Instance.Sizes.Find(s => "Large" == s.Name);
            Toppings.Add(ComponentSingleton.Instance.Toppings.Find(t => "Mozzarella" == t.Name));
            ID = PizzaSingleton.Instance.GetUniquePizzaID();
        }

        public override decimal Price
        {
            get
            {
                decimal price = Crust.Price + Size.Price;
                foreach(Topping topping in Toppings)
                {
                    price += topping.Price;
                }
                return price;
            }
        }

        public override void AddTopping(Topping topping)
        {
            if(Toppings.Exists(t => t.Name == topping.Name))
            {
                Logger.Instance.LogError("Tried to AddTopping() that already existed on pizza " + Name);
            }
            else
            {
                Toppings.Add(topping);
            }
            
        }

        public override void RemoveTopping(Topping topping)
        {
            Toppings.Remove(topping);
        }

        public bool Equals(CustomPizza other)
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

            return ID == other.ID;
        }

        public override bool Equals(object other)
        {
            return this.Equals(other as CustomPizza);
        }

        public static bool operator ==(CustomPizza lhs, CustomPizza rhs)
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

        public static bool operator !=(CustomPizza lhs, CustomPizza rhs)
        {
            return !(lhs == rhs);
        }

        public override int GetHashCode() => ID;
        
    }
}