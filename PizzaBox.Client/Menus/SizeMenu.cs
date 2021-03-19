using System;
using PizzaBox.Client.Abstracts;
using PizzaBox.Client.Singletons;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Singletons;

namespace PizzaBox.Client.Menus
{
    internal class SizeMenu : ASelectOptionMenu
    {
        private static SizeMenu _sizeMenu;
        private int _sizeCount;

        private SizeMenu()
        {
            title = "Select Size";
        }

        public static SizeMenu Instance
        {
            get
            {
                if(_sizeMenu == null)
                {
                    _sizeMenu = new SizeMenu();
                }
                return _sizeMenu;
            }
        }

        public override void Run()
        {
            options.Clear();
            inlineInformation = $"Current Selection: {Session.Instance.OpenPizza.Size.Name}";
            options.Add("");
            _sizeCount = ComponentSingleton.Instance.Sizes.Count;
            foreach (Size size in ComponentSingleton.Instance.Sizes)
            {
                options.Add($"{size.Name,-20} {size.Price,-3:C2}");
            }
            options.Add("Go Back");
            
            var selection = GetSelection();
            if(selection == _sizeCount + 1)
            {   // Go Back Selected
                PizzaBuildingMenu.Instance.Run();
            }
            else
            {
                Session.Instance.OpenPizza.Size = ComponentSingleton.Instance.Sizes[selection-1];
                PizzaBuildingMenu.Instance.Run();
            }
        }

    }
}