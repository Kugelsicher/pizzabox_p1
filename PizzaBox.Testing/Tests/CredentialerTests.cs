using Xunit;
using PizzaBox.Domain.Singletons;
using PizzaBox.Domain.Models;
using System.Text;

namespace PizzaBox.Testing.Tests
{
    public class CredentialerTests
    {
        [Fact]
        public void Test_LoginUser()
        {
            var actual = Credentialer.Instance.GetPermissions(Credentialer.Instance.LogIn("Matthew"));
            var expected = UserType.User;

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Test_LoginStore()
        {
            var actual = Credentialer.Instance.GetPermissions(Credentialer.Instance.LogIn("Timothy"));
            var expected = UserType.Store;

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Test_LoginAdmin()
        {
            var actual = Credentialer.Instance.GetPermissions(Credentialer.Instance.LogIn("Grimsley"));
            var expected = UserType.Admin;

            Assert.Equal(expected, actual);
        }
    }

}