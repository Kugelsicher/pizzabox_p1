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
    public class PizzaSingleton
    {
        private int _nextPizzaID = 1000;
        readonly private string _prebuiltPizzasPath = "PrebuiltPizzas.xml";
        private static PizzaSingleton _pizzaSingleton;
        public List<PrebuiltPizza> PrebuiltPizzas { get; set; }
        public static PizzaSingleton Instance
        {
            get
            {
                if(_pizzaSingleton == null)
                {
                    _pizzaSingleton = new PizzaSingleton();
                }

                return _pizzaSingleton;
            }
        }

        private PizzaSingleton()
        {
            if(File.Exists(_prebuiltPizzasPath))
            {
                PrebuiltPizzas = (List<PrebuiltPizza>)FileStorage.Instance.ReadFromXml<PrebuiltPizza>(_prebuiltPizzasPath);
            }/*
            else
            {
                PrebuiltPizzas = new List<PrebuiltPizza>();
                PrebuiltPizzas.Add(new PrebuiltPizza("Cheese Pizza", "Hand Tossed", "Medium",
                    new List<string> { "Mozzarella"}, 0.00m, _nextPizzaID++));
                PrebuiltPizzas.Add(new PrebuiltPizza("Pepperoni Pizza", "Hand Tossed", "Medium",
                    new List<string> { "Mozzarella", "Pepperoni" }, 0.50m, _nextPizzaID++));
                PrebuiltPizzas.Add(new PrebuiltPizza("Supreme Pizza", "Hand Tossed", "Medium",
                    new List<string> { "Mozzarella", "Pepperoni", "Sausage", "Mushrooms", "Red Onions", "Bell Peppers" }, 2.00m, _nextPizzaID++));
                PrebuiltPizzas.Add(new PrebuiltPizza("Meat Lovers Pizza", "Hand Tossed", "Medium",
                    new List<string> { "Mozzarella", "Pepperoni", "Bacon", "Ham", "Sausage" }, 2.00m, _nextPizzaID++));
                PrebuiltPizzas.Add(new PrebuiltPizza("Vegetarian Pizza", "Hand Tossed", "Medium",
                    new List<string> { "Mozzarella", "Mushrooms", "Red Onions", "Bell Peppers", "Spinach" }, 1.50m, _nextPizzaID++));
                SavePizzas();
            }*/

        }

        public int GetUniquePizzaID()
        {
            return _nextPizzaID++;
        }

        public void AddPrebuiltPizza(PrebuiltPizza prebuiltPizza)
        {
            PrebuiltPizzas.Add(prebuiltPizza);
            SavePizzas();
        }

        private void SavePizzas()
        {
            FileStorage.Instance.WriteToXml<PrebuiltPizza>(PrebuiltPizzas, _prebuiltPizzasPath);
        }
    }
}