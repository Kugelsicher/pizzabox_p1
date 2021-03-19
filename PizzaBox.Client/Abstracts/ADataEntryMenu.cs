using System;

namespace PizzaBox.Client.Abstracts
{
    /// <summary>
    /// 
    /// The password masking code was copied from:
    /// <https://stackoverflow.com/questions/3404421/password-masking-console-application>
    /// </summary>
    internal abstract class ADataEntryMenu : AMenu
    {
        public string GetText(string selectionPrompt)
        {
            string entry = null;
            while(entry == null)
            {
                Console.Write(selectionPrompt);
                entry = Console.ReadLine();
            }
            return entry;   
        }
        public string GetPassword(string selectionPrompt)
        {
            string password = "";
            ConsoleKey key;
            while(password == null)
            {
                Console.Write(selectionPrompt);
                do
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    key = keyInfo.Key;

                    if (key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        Console.Write("\b \b");
                        password = password[0..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        Console.Write("*");
                        password += keyInfo.KeyChar;
                    }
                } while (key != ConsoleKey.Enter);
            }
            return password;
        }
        public int GetInt(string selectionPrompt)
        {
            int entry;
            while(!int.TryParse(Console.ReadLine(), out entry))
            {
                Console.Write(selectionPrompt);
            }
            return entry;
        }

        public float GetFloat(string selectionPrompt)
        {
            float entry;
            while(!float.TryParse(Console.ReadLine(), out entry))
            {
                Console.Write(selectionPrompt);
            }
            return entry;
        }
    }
}