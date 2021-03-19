using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public class Store : IEquatable<Store>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set;}
        public int _nextOrderID = 10000;
        public List<Order> Orders { get; set; }
        public Order Cart { get; set; }

        private Store() {} //Required for XmlSerializer()
        public Store(string name, int _storeID)
        {
            Name = name;
            Orders = new List<Order>();
        }

        public Order CreateNewOrder(string username)
        {
            Cart = new Order(_nextOrderID, username);
            _nextOrderID += 1;
            return Cart;
        }

        public bool PlaceOrder()
        {
            if(Cart == null)
            {
                return false;
            }
            else
            {
                Orders.Add(Cart);
                Cart = null;
                return true;
            }
        }

        public void CancelOrder()
        {
            Cart = null;
        }

        public List<Order> UsersOrders(string username)
        {
            List<Order> usersOrders = new List<Order>();
            foreach(Order order in Orders)
            {
                if(order.Username == username)
                {
                    usersOrders.Add(order);
                }
            }
            return usersOrders;
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(Store other)
        {
            return this.Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) 
            {
                return false;
            }
            Store store = obj as Store;
            if (store == null) 
            {
                return false;
            }
            else
            {
                return Equals(store);
            }
        }

        public static bool operator ==(Store lStore, Store rStore)
        {
            return lStore.Equals(rStore);
        }

        public static bool operator !=(Store lStore, Store rStore)
        {
            return !(lStore.Equals(rStore));
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}