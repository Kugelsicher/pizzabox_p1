using System;
using System.Collections.Generic;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
    /// <summary>
    /// Referenced <https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/how-to-define-value-equality-for-a-type>
    /// while implementing the methods of IEquatable
    /// </summary>
    public abstract class APizza
    {
        public int ID;
        public string Name { get; set; }
        public Crust Crust { get; set; }
        public Size Size { get; set; }
        public List<Topping> Toppings { get; set; }
        
        public APizza()
        {
            Toppings = new List<Topping>();
        }

        public abstract void AddTopping(Topping topping);

        public abstract void RemoveTopping(Topping topping);

        public abstract Decimal GetPrice();

    }
}