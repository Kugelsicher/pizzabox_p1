using System;
using System.Text;
using System.Security.Cryptography;
using PizzaBox.Domain.Singletons;
using PizzaBox.Domain.Models;

namespace PizzaBox.Client.Singletons
{
    internal class Credentials
    {
        private static Credentials _credentials = new Credentials();
        public string Username { get; set; }
        public byte[] Token { get; private set; }
        private Credentials()
        {
            Username = "";
            Token = null;
        }

        public static Credentials Instance
        {
            get
            {
                if(_credentials == null)
                {
                    _credentials = new Credentials();
                }
                return _credentials;
            }
        }

        public void LogOut()
        {
            Credentialer.Instance.LogOut(Token);
        }

        public bool LogIn(string username)
        {
            byte[] token = Credentialer.Instance.LogIn(username);
            if(token == null)
            {
                return false;
            }
            Token = token;
            Username = username;
            return true;
        }

        public string CreateUserAccount(string username)
        {
            return Credentialer.Instance.AddAccount(username);
        }
        
        private byte[] GetHash(byte[] input, byte[] salt)
        {
            using(var hashAlgorithm = new SHA256Managed())
            {
                byte[] saltedInput = new byte[input.Length + salt.Length];
                for (int i = 0; i < input.Length; i += 1)
                {
                    saltedInput[i] = input[i];
                }
                for (int i = 0; i < salt.Length; i += 1)
                {
                    saltedInput[input.Length + i] = salt[i];
                }

                return hashAlgorithm.ComputeHash(saltedInput);
            }
        }
    }
}