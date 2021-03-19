using System;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
    public enum UserType
    {
            Null = -1,
            Admin = 0,
            Store = 1,
            User = 2
    }
    public class Account : IEquatable<Account>
    {
        /// <summary>
        /// Referenced <https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/
        /// statements-expressions-operators/how-to-define-value-equality-for-a-type>
        /// while implementing the methods of IEquatable
        /// </summary>
        public string username;
        public UserType userType;
        public Store store;
        
        private Account() {} //Required for XmlSerializer()
        public Account(string username, UserType userType = UserType.User, Store store = null)
        {
            this.username = username;
            this.userType = userType;
            this.store = store;
        }

        public bool Equals(Account other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.GetType() != other.GetType())
            {
                return false;
            }

            return username == other.username;
        }

        public override bool Equals(object other)
        {
            return this.Equals(other as Account);
        }

        public static bool operator ==(Account lhs, Account rhs)
        {
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    return true;
                }

                return false;
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Account lhs, Account rhs)
        {
            return !(lhs == rhs);
        }

        public override int GetHashCode()
        {
            return username.GetHashCode();
        }

    }
}