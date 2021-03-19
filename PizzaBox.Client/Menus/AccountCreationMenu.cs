using System;
using PizzaBox.Client.Abstracts;
using PizzaBox.Client.Singletons;

namespace PizzaBox.Client.Menus
{
    internal class AccountCreationMenu : ADataEntryMenu
    {
        private static AccountCreationMenu _accountCreationMenu;

        private AccountCreationMenu()
        {
            title = "Create Account";
        }

        public static AccountCreationMenu Instance
        {
            get
            {
                if(_accountCreationMenu == null)
                {
                    _accountCreationMenu = new AccountCreationMenu();
                }
                return _accountCreationMenu;
            }
        }

        public override void Run()
        {
            string username = GetText("Please Enter a username:");
            string errorMsg = Credentials.Instance.CreateUserAccount(username);
            
            while(errorMsg != "")
            {
                Console.WriteLine(errorMsg);
                username = GetText("Please Enter a username:");
                errorMsg = Credentials.Instance.CreateUserAccount(username);
            }

            Credentials.Instance.LogIn(username);
            StoreSelectionMenu.Instance.Run();
        }
    }
}