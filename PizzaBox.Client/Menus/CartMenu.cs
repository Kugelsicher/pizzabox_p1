using System;
using PizzaBox.Client.Abstracts;
using PizzaBox.Client.Singletons;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;

namespace PizzaBox.Client.Menus
{
    internal class CartMenu : ASelectOptionMenu
    {
        private static CartMenu _cartMenu;
        private int _prebuiltPizzaCount;
        private int _customPizzaCount;
        private CartMenu()
        {
            title = "Cart";
        }

        public static CartMenu Instance
        {
            get
            {
                if(_cartMenu == null)
                {
                    _cartMenu = new CartMenu();
                }
                return _cartMenu;
            }
        }

        public override void Run()
        {
            options.Clear();
            decimal totalPrice = 0;
            _prebuiltPizzaCount = Session.Instance.Order.PrebuiltPizzas.Count;
            _customPizzaCount = Session.Instance.Order.CustomPizzas.Count;
            foreach (PrebuiltPizza pizza in Session.Instance.Order.PrebuiltPizzas)
            {
                options.Add(pizza.ToString());
                totalPrice += pizza.Price;
            }
            foreach (CustomPizza pizza in Session.Instance.Order.CustomPizzas)
            {
                string pizzaDescription = $"{pizza.Size} {pizza.Crust} {pizza.Price,-3:C2} ";
                foreach (Topping topping in pizza.Toppings)
                {
                    pizzaDescription += topping.Name + ", ";
                }
                options.Add(pizzaDescription);
                totalPrice += pizza.Price;
            }
            inlineInformation = $"Total: {totalPrice,-3:C2}";
            options.Add("");
            options.Add("Add Pizza");
            options.Add("Place Order");
            options.Add("Cancel Order");
            options.Add("View Order History");

            var selection = GetSelection();

            if(selection > _prebuiltPizzaCount + _customPizzaCount)
            {
                switch(selection - _prebuiltPizzaCount - _customPizzaCount)
                {
                    case 1: // Add Pizza Selected
                        PizzaSelectionMenu.Instance.Run();
                    break;
                    case 2: // Place Order Selected
                        if(_prebuiltPizzaCount + _customPizzaCount > 0)
                        {
                            if(totalPrice > 250m)
                            {
                                System.Console.WriteLine("Cannot place order. The maximum order total is $250.00!");
                                CartMenu.Instance.Run();
                            }
                            else
                            {
                                if(_prebuiltPizzaCount + _customPizzaCount > 50)
                                {
                                    System.Console.WriteLine("Cannot place order. The maximum number of pizzas is 50!");
                                    CartMenu.Instance.Run();
                                }
                                else
                                {
                                    if(Session.Instance.Store.PlaceOrder())
                                    {
                                        System.Console.WriteLine("Order Placed. Pizzas are on the way!");
                                        StoreSelectionMenu.Instance.Run();
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("We're sorry, something went wrong! Your order has been canceled.");
                                        CancelOrder();
                                    }
                                }
                            }
                        }
                        else
                        {
                            System.Console.WriteLine("Cannot place order. Your cart is empty!");
                            CartMenu.Instance.Run();
                        }
                    break;
                    case 3: // Cancel Order Selected
                        CancelOrder();
                    break;
                    case 4: // View Order History Selected
                        UserHistoryMenu.Instance.Run();
                    break;
                }
            }
            else
            {
                if(selection <= Session.Instance.Order.PrebuiltPizzas.Count)
                {   // PrebuiltPizza was selected
                    Session.Instance.SetOpenPizza(Session.Instance.Order.PrebuiltPizzas[selection-1]);
                    PizzaBuildingMenu.Instance.Run();
                }
                else
                {   // CustomPizza was selected
                    Session.Instance.SetOpenPizza(Session.Instance.Order.CustomPizzas[selection-_prebuiltPizzaCount-1]);
                    PizzaBuildingMenu.Instance.Run();
                }
            }
        }

        private void CancelOrder()
        {
            Session.Instance.Store.CancelOrder();
            Session.Instance.Store = null;
            Session.Instance.Order = null;
            StoreSelectionMenu.Instance.Run();
        }

    }
}