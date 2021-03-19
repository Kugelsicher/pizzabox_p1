using System;
using System.Collections.Generic;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
    public abstract class APizza
    {
        public int ID;
        public string Name { get; set; }
        public Crust Crust { get; set; }
        public Size Size { get; set; }
        public List<Topping> Toppings { get; set; }
        public abstract decimal Price { get; }
        
        public APizza()
        {
            Toppings = new List<Topping>();
        }

        public abstract void AddTopping(Topping topping);

        public abstract void RemoveTopping(Topping topping);

        public override string ToString()
        {
            return $"{Size} {Name:20} {Crust} {Price,-3:C2}";
        }
    }
}