using System;
using PizzaBox.Client.Abstracts;
using PizzaBox.Client.Singletons;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;

namespace PizzaBox.Client.Menus
{
    internal class ViewOrderMenu : ASelectOptionMenu
    {
        private static ViewOrderMenu _viewOrderMenu;
        private int _prebuiltPizzaCount;
        private int _customPizzaCount;
        private ViewOrderMenu()
        {
            title = "";
        }

        public static ViewOrderMenu Instance
        {
            get
            {
                if(_viewOrderMenu == null)
                {
                    _viewOrderMenu = new ViewOrderMenu();
                }
                return _viewOrderMenu;
            }
        }

        public override void Run()
        {
            Console.WriteLine($"Viewing Order #{Session.Instance.ViewingOrder.OrderID}");
            options.Clear();
            decimal totalPrice = 0;
            _prebuiltPizzaCount = Session.Instance.ViewingOrder.PrebuiltPizzas.Count;
            _customPizzaCount = Session.Instance.ViewingOrder.CustomPizzas.Count;
            foreach (PrebuiltPizza pizza in Session.Instance.ViewingOrder.PrebuiltPizzas)
            {
                Console.WriteLine($"{pizza.Size} {pizza.Name:20} {pizza.Crust} {pizza.GetPrice(),-3:C2}");
                totalPrice += pizza.GetPrice();
            }
            foreach (CustomPizza pizza in Session.Instance.ViewingOrder.CustomPizzas)
            {
                string pizzaDescription = $"{pizza.Size} {pizza.Crust} {pizza.GetPrice(),-3:C2} ";
                foreach (Topping topping in pizza.Toppings)
                {
                    pizzaDescription += topping.Name + ", ";
                }
                Console.WriteLine(pizzaDescription);
                totalPrice += pizza.GetPrice();
            }
            inlineInformation = $"Total: {totalPrice,-3:C2}";
            options.Add("");
            options.Add("Go Back");

            var selection = GetSelection();

            UserHistoryMenu.Instance.Run();
        }
    }
}