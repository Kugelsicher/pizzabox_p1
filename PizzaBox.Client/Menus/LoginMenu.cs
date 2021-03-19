using System;
using PizzaBox.Client.Abstracts;
using PizzaBox.Client.Singletons;
using PizzaBox.Domain.Singletons;

namespace PizzaBox.Client.Menus
{
    internal class LoginMenu : ADataEntryMenu
    {
        private static LoginMenu _loginMenu;

        private LoginMenu()
        {
            title = "Login";
        }

        public static LoginMenu Instance
        {
            get
            {
                if(_loginMenu == null)
                {
                    _loginMenu = new LoginMenu();
                }
                return _loginMenu;
            }
        }

        public override void Run()
        {
            string username = GetText("Please Enter your username:");    

            if(Credentials.Instance.LogIn(username))
            {
                Console.WriteLine($"Permissions: {Credentialer.Instance.GetPermissions(Credentials.Instance.Token)}");
                StoreSelectionMenu.Instance.Run();
            }
            else
            {
                Console.WriteLine("Invalid username!");
                CredentialsMenu.Instance.Run();
            }            
        }
    }
}