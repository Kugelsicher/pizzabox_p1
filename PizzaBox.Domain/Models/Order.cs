using System;
using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Storing;

namespace PizzaBox.Domain.Models
{
    /// <summary>
    /// Referenced <https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/how-to-define-value-equality-for-a-type>
    /// while implementing the methods of IEquatable
    /// </summary>
    public class Order : IEquatable<Order>
    {
        public int OrderID { get; set; }
        public List<PrebuiltPizza> PrebuiltPizzas { get; set; }
        public List<CustomPizza> CustomPizzas { get; set; }
        public string Username { get; set; }
        private Order() {} //Required for XmlSerializer()
        public Order(int orderID, string username)
        {
            OrderID = orderID;
            Username = username;
            PrebuiltPizzas = new List<PrebuiltPizza>();
            CustomPizzas = new List<CustomPizza>();
        }

        public void AddPizza(APizza pizza)
        {
            if(pizza.GetType() == typeof(PrebuiltPizza))
            {
                PrebuiltPizzas.Add((PrebuiltPizza)pizza);
            }
            else if(pizza.GetType() == typeof(CustomPizza))
            {
                CustomPizzas.Add((CustomPizza)pizza);
            }
        }

        public void RemovePizza(APizza pizza)
        {
            if(pizza.GetType() == typeof(PrebuiltPizza))
            {
                if(PrebuiltPizzas.Exists(p => p.ID == pizza.ID))
                {
                    PrebuiltPizzas.Remove((PrebuiltPizza)pizza);
                }
                else
                {
                    Logger.Instance.LogError("Tried to RemovePizza() a " + pizza.Name + " that did not exist in order " + OrderID);
                }
            }
            else if(pizza.GetType() == typeof(CustomPizza))
            {
                if(CustomPizzas.Exists(p => p.ID == pizza.ID))
                {
                    CustomPizzas.Remove((CustomPizza)pizza);
                }
                else
                {
                    Logger.Instance.LogError("Tried to RemovePizza() a pizza" + pizza.Name + "  that did not exist in order " + OrderID);
                }
            }
            else
            {
                Logger.Instance.LogError("Tried to RemovePizza() a " + pizza.GetType() + ", instead of a pizza type.");
            }
        }

        public bool Equals(Order other)
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

            return OrderID == other.OrderID;
        }

        public override bool Equals(object other)
        {
            return this.Equals(other as Order);
        }

        public static bool operator ==(Order lhs, Order rhs)
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

        public static bool operator !=(Order lhs, Order rhs)
        {
            return !(lhs == rhs);
        }
        
        public override int GetHashCode()
        {
            return OrderID;
        }
    }
}