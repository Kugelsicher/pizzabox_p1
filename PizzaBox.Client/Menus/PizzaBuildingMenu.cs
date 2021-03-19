using System;
using PizzaBox.Client.Abstracts;
using PizzaBox.Client.Singletons;
using PizzaBox.Domain.Models;
using PizzaBox.Storing;

namespace PizzaBox.Client.Menus
{
    internal class PizzaBuildingMenu : ASelectOptionMenu
    {
        private static PizzaBuildingMenu _pizzaBuildingMenu;

        private PizzaBuildingMenu()
        {
            title = "Pizza Builder";
        }

        public static PizzaBuildingMenu Instance
        {
            get
            {
                if(_pizzaBuildingMenu == null)
                {
                    _pizzaBuildingMenu = new PizzaBuildingMenu();
                }
                return _pizzaBuildingMenu;
            }
        }
        
        public override void Run()
        {
            options.Clear();
            if(Session.Instance.OpenPizza.GetType() == typeof(PrebuiltPizza))
            {
                inlineInformation = $"{Session.Instance.OpenPizza.Name} Price: {Session.Instance.OpenPizza.GetPrice(),-3:C2}";
                options.Add("");
                options.Add("Choose Crust\tCurrent: " + Session.Instance.OpenPizza.Crust.Name);
                options.Add("Choose Size\t\tCurrent: " + Session.Instance.OpenPizza.Size.Name);
                options.Add("Add Pizza to Order");
                options.Add("Remove Pizza from Order");

                var selection = GetSelection();
                switch (selection)
                {
                    case 1:
                        CrustMenu.Instance.Run();
                    break;
                    case 2:
                        SizeMenu.Instance.Run();
                    break;
                    case 3:
                        CartMenu.Instance.Run();
                    break;
                    case 4:
                        Session.Instance.DeleteOpenPizza();
                        CartMenu.Instance.Run();
                    break;
                }
            }
            else if(Session.Instance.OpenPizza.GetType() == typeof(CustomPizza))
            {
                string listOfToppings = "";
                foreach (Topping topping in Session.Instance.OpenPizza.Toppings)
                {
                    listOfToppings += topping.Name + ", ";
                }
                options.Add("Add/Remove Toppings\tCurrent: " + listOfToppings);
                options.Add("Choose Crust\t\tCurrent: " + Session.Instance.OpenPizza.Crust.Name);
                options.Add("Choose Size\t\t\tCurrent: " + Session.Instance.OpenPizza.Size.Name);
                options.Add("Add Pizza to Order");
                options.Add("Remove Pizza from Order");

                var selection = GetSelection();
                switch (selection)
                {
                    case 1:
                        ToppingsMenu.Instance.Run();
                    break;
                    case 2:
                        CrustMenu.Instance.Run();
                    break;
                    case 3:
                        SizeMenu.Instance.Run();
                    break;
                    case 4:
                        CartMenu.Instance.Run();
                    break;
                    case 5:
                        Session.Instance.DeleteOpenPizza();
                        CartMenu.Instance.Run();
                    break;
                }
            }
            else
            {
                Logger.Instance.LogError("PizzaBuildingMenu was Run without a valid object stored in Session.Instance.OpenPizza");
            }
        }

    }
}