using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Storing;
using System.Text;
using System.IO;

namespace PizzaBox.Domain.Singletons
{
    /// <summary>
    /// 
    /// Referenced <https://stackoverflow.com/questions/2138429/hash-and-salt-passwords-in-c-sharp> 
    /// while write the code for hashing a salted password.
    /// </summary>
    public class Credentialer
    {
        readonly private string _storagePath = "Accounts.xml";
        private static Credentialer _credentialer;
        public List<Account> Accounts { get; set; }
        private Dictionary<byte[] /*token*/, Account> _activeUsers;
        private Credentialer()
        {
            _activeUsers = new Dictionary<byte[], Account>();
            if(File.Exists(_storagePath))
            {
                LoadAccounts();
            }
            else
            {
                Accounts = new List<Account>();
            }
        }
        public static Credentialer Instance
        {
            get
            {
                if(_credentialer == null)
                {
                    _credentialer = new Credentialer();
                }

                return _credentialer;
            }
        }
        private void LoadAccounts()
        {
            Accounts = (List<Account>)FileStorage.Instance.ReadFromXml<Account>(_storagePath);
        }
        
        private void SaveAccounts()
        {
            FileStorage.Instance.WriteToXml<Account>(Accounts, _storagePath);
        }

        public string AddAccount(string username)
        {
            if(Accounts.Exists(a => a.username == username))
            {
                return "Username already exists.";
            }

            Accounts.Add(new Account(username, UserType.User));
            SaveAccounts();

            return "";
        }

        public byte[] LogIn(string username)
        {
            Account user = Accounts.Find(account => account.username == username);
            if(user != null)
            {
                byte[] token = Guid.NewGuid().ToByteArray();
                _activeUsers.Add(token, user);
                return token;
            }
            return null;
        }

        public bool LogOut(byte[] token)
        {
            return _activeUsers.Remove(token);
        }

        public UserType GetPermissions(byte[] token)
        {
            try
            {
                return _activeUsers[token].userType;
            }
            catch(ArgumentException)
            {
                return UserType.Null;
            }
            catch(KeyNotFoundException)
            {
                return UserType.Null;
            }
        }

        public Store GetEmployeesStore(byte[] token)
        {
            try
            {
                return _activeUsers[token].store;
            }
            catch(ArgumentException)
            {
                return null;
            }
            catch(KeyNotFoundException)
            {
                return null;
            }
        }

    }

}