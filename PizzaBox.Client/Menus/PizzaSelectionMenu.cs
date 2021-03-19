using System;
using PizzaBox.Client.Abstracts;
using PizzaBox.Client.Singletons;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Singletons;

namespace PizzaBox.Client.Menus
{
    internal class PizzaSelectionMenu : ASelectOptionMenu
    {
        private static PizzaSelectionMenu _pizzaSelectionMenu;
        private int _prebuiltPizzaCount;

        private PizzaSelectionMenu()
        {
            title = "Select a Pizza:";
        }

        public static PizzaSelectionMenu Instance
        {
            get
            {
                if(_pizzaSelectionMenu == null)
                {
                    _pizzaSelectionMenu = new PizzaSelectionMenu();
                }
                return _pizzaSelectionMenu;
            }
        }

        public override void Run()
        {
            options.Clear();
            _prebuiltPizzaCount = PizzaSingleton.Instance.PrebuiltPizzas.Count;
            options.Add("Build Your Own Pizza");
            inlineInformation = "Or Select from one of our Favorites:";
            options.Add("");
            foreach (PrebuiltPizza prebuiltPizza in PizzaSingleton.Instance.PrebuiltPizzas)
            {
                options.Add($"{prebuiltPizza.Name,-20} {prebuiltPizza.GetPrice(),-3:C2}");
            }
            options.Add("Go Back");

            var selection = GetSelection();
            if(selection == 1)
            {   // Build Your Own Pizza
                
                Session.Instance.NewOpenPizza(new CustomPizza());
                PizzaBuildingMenu.Instance.Run();
            }
            if(selection == _prebuiltPizzaCount + 2)
            {   // Go Back
                CartMenu.Instance.Run();
            }
            else
            {   // Selected PrebuiltPizza
                Session.Instance.NewOpenPizza(new PrebuiltPizza(PizzaSingleton.Instance.PrebuiltPizzas[selection-2]));
                PizzaBuildingMenu.Instance.Run();
            }
        }
    }
}