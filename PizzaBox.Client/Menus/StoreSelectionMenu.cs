using System;
using PizzaBox.Client.Abstracts;
using PizzaBox.Client.Singletons;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Singletons;

namespace PizzaBox.Client.Menus
{
    internal class StoreSelectionMenu : ASelectOptionMenu
    {
        private static StoreSelectionMenu _storeSelectionMenu;
        private int _storeCount;

        private StoreSelectionMenu()
        {
            title = "Select a Store";
        }

        public static StoreSelectionMenu Instance
        {
            get
            {
                if(_storeSelectionMenu == null)
                {
                    _storeSelectionMenu = new StoreSelectionMenu();
                }
                return _storeSelectionMenu;
            }
        }

        public override void Run()
        {
            _storeCount = StoreSingleton.Instance.Stores.Count;
            options.Clear();
            foreach (Store store in StoreSingleton.Instance.Stores)
            {
                options.Add(store.Name);
            }
            options.Add("Go Back");

            var selection = GetSelection();
            if(selection == _storeCount + 1)
            {
                CredentialsMenu.Instance.Run();
            }
            else
            {
                Session.Instance.Store = StoreSingleton.Instance.Stores[selection-1];
                Session.Instance.Order = Session.Instance.Store.CreateNewOrder(Credentials.Instance.Username);
                CartMenu.Instance.Run();
            }
        }

    }
}