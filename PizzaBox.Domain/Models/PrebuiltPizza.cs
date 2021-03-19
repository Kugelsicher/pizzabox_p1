using System;
using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Singletons;
using PizzaBox.Storing;

namespace PizzaBox.Domain.Models
{
    public class PrebuiltPizza : APizza, IEquatable<PrebuiltPizza>
    {
        /// <summary>
        /// Referenced <https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/how-to-define-value-equality-for-a-type>
        /// while implementing the methods of IEquatable
        /// </summary>
        public Decimal _prebuiltPrice;
        private PrebuiltPizza() {} //Required for XmlSerializer()
        public PrebuiltPizza(string name, string crustName, string sizeName, List<string> toppingNames, Decimal price)
        {
            Name = name;
            _prebuiltPrice = price;
            Crust = ComponentSingleton.Instance.Crusts.Find(c => crustName == c.Name);
            if(Crust == null)
            {
                Logger.Instance.LogError("PrebuiltPizza, " + name + ", tried to add a crust, " + Crust + ", that does not exist");
            }
            Size = ComponentSingleton.Instance.Sizes.Find(s => sizeName == s.Name);
            if(Size == null)
            {
                Logger.Instance.LogError("PrebuiltPizza, " + name + ", tried to add a crust, " + Size + ", that does not exist");
            }
            foreach(var toppingName in toppingNames)
            { 
                Topping newTopping = ComponentSingleton.Instance.Toppings.Find(t => toppingName == t.Name);
                if(newTopping == null)
                {
                    Logger.Instance.LogError("PrebuiltPizza, " + name + ", tried to add a topping, " + toppingName + ", that does not exist");
                }
                else
                {
                    Toppings.Add(newTopping);
                }
            }
            ID = PizzaSingleton.Instance.GetUniquePizzaID();
        }
        public PrebuiltPizza(string name, string crustName, string sizeName, List<string> toppingNames, Decimal price, int pizzaID)
        {
            Name = name;
            _prebuiltPrice = price;
            Crust = ComponentSingleton.Instance.Crusts.Find(c => crustName == c.Name);
            if(Crust == null)
            {
                Logger.Instance.LogError("PrebuiltPizza, " + name + ", tried to add a crust, " + Crust + ", that does not exist");
            }
            Size = ComponentSingleton.Instance.Sizes.Find(s => sizeName == s.Name);
            if(Size == null)
            {
                Logger.Instance.LogError("PrebuiltPizza, " + name + ", tried to add a crust, " + Size + ", that does not exist");
            }
            foreach(var toppingName in toppingNames)
            { 
                Topping newTopping = ComponentSingleton.Instance.Toppings.Find(t => toppingName == t.Name);
                if(newTopping == null)
                {
                    Logger.Instance.LogError("PrebuiltPizza, " + name + ", tried to add a topping, " + toppingName + ", that does not exist");
                }
                else
                {
                    Toppings.Add(newTopping);
                }
            }
            ID = pizzaID;
        }
        public PrebuiltPizza(PrebuiltPizza pizza)
        {
            _prebuiltPrice = pizza._prebuiltPrice;
            Name = pizza.Name;
            Crust = pizza.Crust;
            Size = pizza.Size;
            Toppings = pizza.Toppings;
            ID = PizzaSingleton.Instance.GetUniquePizzaID();
        }

        public override decimal GetPrice()
        {
            return _prebuiltPrice + Crust.Price + Size.Price;
        }

        public override void AddTopping(Topping topping)
        {
            Logger.Instance.LogError("Tried to AddTopping() for prebuilt pizza " + Name);
        }

        public override void RemoveTopping(Topping topping)
        {
            Logger.Instance.LogError("Tried to RemoveTopping() for prebuilt pizza " + Name);
        }
        
        public bool Equals(PrebuiltPizza other)
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
            return this.Equals(other as PrebuiltPizza);
        }

        public static bool operator ==(PrebuiltPizza lhs, PrebuiltPizza rhs)
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

        public static bool operator !=(PrebuiltPizza lhs, PrebuiltPizza rhs)
        {
            return !(lhs == rhs);
        }

        public override int GetHashCode() => ID;
        
    }
}