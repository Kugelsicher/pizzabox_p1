using System;
using System.Collections.Generic;
using PizzaBox.Client.Abstracts;
using PizzaBox.Client.Singletons;
using PizzaBox.Domain.Models;

namespace PizzaBox.Client.Menus
{
    internal class UserHistoryMenu : ASelectOptionMenu
    {
        private static UserHistoryMenu _userHistoryMenu;

        private UserHistoryMenu()
        {
            title = $"Your Order History at {Session.Instance.Store.Name}:";
        }

        public static UserHistoryMenu Instance
        {
            get
            {
                if(_userHistoryMenu == null)
                {
                    _userHistoryMenu = new UserHistoryMenu();
                }
                return _userHistoryMenu;
            }
        }

        public override void Run()
        {
            List<Order> usersOrders = Session.Instance.Store.UsersOrders(Credentials.Instance.Username);
            
            options.Clear();
            foreach (Order order in usersOrders)
            {
                options.Add($"{order.PrebuiltPizzas.Count + order.CustomPizzas.Count} Pizzas in Order # {order.OrderID}");
            }
            options.Add("Go Back");
            
            var selection = GetSelection();
            if(selection == usersOrders.Count + 1)
            {   // Go Back Selected
                CartMenu.Instance.Run();
            }
            else
            {
                Session.Instance.ViewingOrder = usersOrders[selection-1];
                ViewOrderMenu.Instance.Run();
            }
        }

    }
}