using System;
using PizzaBox.Client.Abstracts;
using PizzaBox.Client.Singletons;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Singletons;

namespace PizzaBox.Client.Menus
{
    internal class ToppingsMenu : ASelectOptionMenu
    {
        private static ToppingsMenu _toppingsMenu;
        private int _toppingsCount;

        private ToppingsMenu()
        {
            title = "Select a Topping to Add or Remove it:";
        }

        public static ToppingsMenu Instance
        {
            get
            {
                if(_toppingsMenu == null)
                {
                    _toppingsMenu = new ToppingsMenu();
                }
                return _toppingsMenu;
            }
        }

        public override void Run()
        {
            options.Clear();
            inlineInformation = "Current Toppings: ";
            if(Session.Instance.OpenPizza.Toppings.Count == 0)
            {
                inlineInformation += "NONE!";
            }
            else
            {
                foreach (Topping topping in Session.Instance.OpenPizza.Toppings)
                {
                    inlineInformation += $"{topping.Name}, ";
                }
            }
            options.Add("");
            
            _toppingsCount = ComponentSingleton.Instance.Toppings.Count;
            foreach (Topping topping in ComponentSingleton.Instance.Toppings)
            {
                options.Add($"{topping.Name,-20} {topping.Price,-3:C2}");
            }
            options.Add("Go Back");
            
            var selection = GetSelection();
            if(selection == _toppingsCount + 1)
            {   // Go Back Selected
                PizzaBuildingMenu.Instance.Run();
            }
            else
            {
                bool pizzaAlreadyHasTopping = false;
                foreach (Topping topping in Session.Instance.OpenPizza.Toppings)
                {
                    if(topping == ComponentSingleton.Instance.Toppings[selection-1])
                    {
                        pizzaAlreadyHasTopping = true;
                        break;
                    }
                }

                if(pizzaAlreadyHasTopping)
                {
                    Session.Instance.OpenPizza.RemoveTopping(ComponentSingleton.Instance.Toppings[selection-1]);
                }
                else
                {
                    if(Session.Instance.OpenPizza.Toppings.Count > 4)
                    {
                        System.Console.WriteLine("Pizzas can have at most 5 toppings!");
                    }
                    else
                    {
                        Session.Instance.OpenPizza.AddTopping(ComponentSingleton.Instance.Toppings[selection-1]);
                    }
                }
                ToppingsMenu.Instance.Run();
            }
        }

    }
}