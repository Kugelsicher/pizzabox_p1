using System.Collections.Generic;
using System.IO;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Storing;

namespace PizzaBox.Domain.Singletons
{
    /// <summary>
    /// 
    /// </summary>
    public class ComponentSingleton
    {
        private string _crustsPath = "Crusts.xml";
        private string _toppingsPath = "Toppings.xml";
        private string _sizesPath = "Sizes.xml";
        private static ComponentSingleton _componentSingleton;
        public List<Crust> Crusts { get; set; }
        public List<Topping> Toppings { get; set; }
        public List<Size> Sizes { get; set; }
        public static ComponentSingleton Instance
        {
            get
            {
                if(_componentSingleton == null)
                {
                    _componentSingleton = new ComponentSingleton();
                }

                return _componentSingleton;
            }
        }

        private ComponentSingleton()
        {
            if(File.Exists(_crustsPath))
            {
                Crusts = (List<Crust>)FileStorage.Instance.ReadFromXml<Crust>(_crustsPath);
            }/*
            else
            {
                Crusts = new List<Crust>();
                Crusts.Add(new Crust("Hand Tossed", 0.00m));
                Crusts.Add(new Crust("Cheese Stuffed", 2.00m));
                Crusts.Add(new Crust("Deep Dish", 1.00m));
                FileStorage.Instance.WriteToXml<Crust>(Crusts, _crustsPath);
            }*/

            if(File.Exists(_toppingsPath))
            {
                Toppings = (List<Topping>)FileStorage.Instance.ReadFromXml<Topping>(_toppingsPath);
            }/*
            else
            {
                Toppings = new List<Topping>();
                Toppings.Add(new Topping("Mozzarella", 0.00m));
                Toppings.Add(new Topping("Cheddar", 0.60m));
                Toppings.Add(new Topping("Pepperoni", 0.80m));
                Toppings.Add(new Topping("Bacon", 0.80m));
                Toppings.Add(new Topping("Chicken", 0.80m));
                Toppings.Add(new Topping("Ham", 0.80m));
                Toppings.Add(new Topping("Sausage", 0.80m));
                Toppings.Add(new Topping("Pineapple", 0.60m));
                Toppings.Add(new Topping("Mushrooms", 0.60m));
                Toppings.Add(new Topping("Black Olives", 0.60m));
                Toppings.Add(new Topping("Red Onions", 0.60m));
                Toppings.Add(new Topping("Bell Peppers", 0.60m));
                Toppings.Add(new Topping("Spinach", 0.60m));
                FileStorage.Instance.WriteToXml<Topping>(Toppings, _toppingsPath);
            }*/
            
            if(File.Exists(_sizesPath))
            {
                Sizes = (List<Size>)FileStorage.Instance.ReadFromXml<Size>(_sizesPath);
            }/*
            else
            {
                Sizes = new List<Size>();
                Sizes.Add(new Size("Personal", 5.00m));
                Sizes.Add(new Size("Medium", 9.00m));
                Sizes.Add(new Size("Large", 12.00m));
                Sizes.Add(new Size("Extra Large", 15.00m));
                FileStorage.Instance.WriteToXml<Size>(Sizes, _sizesPath);
            }*/
        }
        
        private void SaveComponents()
        {
            FileStorage.Instance.WriteToXml<Crust>(Crusts, _crustsPath);
            FileStorage.Instance.WriteToXml<Topping>(Toppings, _toppingsPath);
            FileStorage.Instance.WriteToXml<Size>(Sizes, _sizesPath);
        }
    }
}