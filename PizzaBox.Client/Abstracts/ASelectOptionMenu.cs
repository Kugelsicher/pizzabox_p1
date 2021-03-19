using System;
using System.Collections.Generic;

namespace PizzaBox.Client.Abstracts
{
    internal abstract class ASelectOptionMenu : AMenu
    {
        protected string inlineInformation;
        protected List<string> options;
        private readonly string selectionPrompt = "Please Enter the number that corresponds to your selection:";
        protected ASelectOptionMenu()
        {
            inlineInformation = "";
            options = new List<string>();
        }
        public int GetSelection()
        {
            Console.WriteLine(title);

            int number = 1;
            foreach(string option in options)
            {
                if(option == "")
                {
                    Console.WriteLine(inlineInformation);
                }
                else
                {
                    Console.WriteLine(number + " : " + option);
                    number += 1;
                }
            }
            Console.WriteLine(selectionPrompt);
            
            return GetValidSelectionInput();
        }
        private int GetValidSelectionInput()
        {
            int selection;
            while(!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > options.Count)
            {
                Console.WriteLine("Invalid Entry!\n"+selectionPrompt);
            }

            return selection;
        }
    }
}