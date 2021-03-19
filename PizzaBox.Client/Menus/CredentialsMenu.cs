using System;
using PizzaBox.Client.Abstracts;

namespace PizzaBox.Client.Menus
{
    internal class CredentialsMenu : ASelectOptionMenu
    {
        private static CredentialsMenu _credentialsMenu;

        private CredentialsMenu()
        {
            title = "Credentials Menu";
            options.Add("Login");
            options.Add("Create Account");
            options.Add("Exit PizzaBox Application");
        }
        public static CredentialsMenu Instance
        {
            get
            {
                if(_credentialsMenu == null)
                {
                    _credentialsMenu = new CredentialsMenu();
                }
                return _credentialsMenu;
            }
        }

        public override void Run()
        {
            var selection = GetSelection();
            switch (selection)
            {
                case 1:
                    LoginMenu.Instance.Run();
                break;
                case 2:
                    AccountCreationMenu.Instance.Run();
                break;
                case 3:
                    Environment.Exit(0);
                break;
            }
            Environment.Exit(1);
        }

    }
}