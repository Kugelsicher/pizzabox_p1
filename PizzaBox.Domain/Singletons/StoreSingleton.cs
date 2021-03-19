using System.Collections.Generic;
using System.IO;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Storing;

namespace PizzaBox.Domain.Singletons
{
    /// <summary>
    /// 
    /// </summary>
    public class StoreSingleton
    {
        readonly private string _storesPath = "Stores.xml";
        private static StoreSingleton _storeSingleton;
        public List<Store> Stores { get; set; }
        public static StoreSingleton Instance
        {
            get
            {
                if(_storeSingleton == null)
                {
                    _storeSingleton = new StoreSingleton();
                }

                return _storeSingleton;
            }
        }

        private StoreSingleton()
        {
            if(File.Exists(_storesPath))
            {
                LoadStores();
            }/*
            else
            {
                Stores = new List<Store>();
                Stores.Add(new Store("Steve's Pizzeria", 192));
                Stores.Add(new Store("Panucci's Pizza", 136));
                SaveStores();
            }*/
        }

        private void LoadStores()
        {
            Stores = (List<Store>)FileStorage.Instance.ReadFromXml<Store>(_storesPath);
        }
        
        private void SaveStores()
        {
            FileStorage.Instance.WriteToXml<Store>(Stores, _storesPath);
        }
    }
}