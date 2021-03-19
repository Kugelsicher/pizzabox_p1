using System;
using PizzaBox.Client.Abstracts;
using PizzaBox.Client.Singletons;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Singletons;

namespace PizzaBox.Client.Menus
{
    internal class CrustMenu : ASelectOptionMenu
    {
        private static CrustMenu _crustMenu;
        private int _crustCount;

        private CrustMenu()
        {
            title = "Select Crust";
        }

        public static CrustMenu Instance
        {
            get
            {
                if(_crustMenu == null)
                {
                    _crustMenu = new CrustMenu();
                }
                return _crustMenu;
            }
        }

        public override void Run()
        {
            options.Clear();
            inlineInformation = $"Current Selection: {Session.Instance.OpenPizza.Crust.Name}";
            options.Add("");
            _crustCount = ComponentSingleton.Instance.Crusts.Count;
            foreach (Crust crust in ComponentSingleton.Instance.Crusts)
            {
                options.Add($"{crust.Name,-20} {crust.Price,-3:C2}");
            }
            options.Add("Go Back");
            
            var selection = GetSelection();
            if(selection == _crustCount + 1)
            {   // Go Back Selected
                PizzaBuildingMenu.Instance.Run();
            }
            else
            {
                Session.Instance.OpenPizza.Crust = ComponentSingleton.Instance.Crusts[selection-1];
                PizzaBuildingMenu.Instance.Run();
            }
        }

    }
}