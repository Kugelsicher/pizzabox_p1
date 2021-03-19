using System;
using PizzaBox.Client.Menus;

namespace PizzaBox.Client
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            CredentialsMenu.Instance.Run();
        }
    }
}