using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Storing;

namespace PizzaBox.Client.Singletons
{
    internal class Session
    {
        private static Session _session = new Session();
        public Store Store { get; set; }
        public Order Order { get; set; }
        public Order ViewingOrder { get; set; }
        public APizza OpenPizza { get; set; }
        private Session() { }

        public static Session Instance
        {
            get
            {
                if(_session == null)
                {
                    _session = new Session();
                }
                return _session;
            }
        }

        public void NewOpenPizza(APizza pizza)
        {
            Order.AddPizza(pizza);
            OpenPizza = pizza;
        }

        public void SetOpenPizza(APizza pizza)
        {
            OpenPizza = pizza;
        }

        public void DeleteOpenPizza()
        {
            Order.RemovePizza(OpenPizza);
            OpenPizza = null;
        }

    }
}