using System;
using PizzaBox.Domain.Models;
using Xunit;

namespace PizzaBox.Testing.Tests
{
    public class StoreTests
    {
        [Fact]
        public void StoreCreation()
        {
            var expected = "Mello Mush";
            var store = new Store(expected, 954);

            var actual = store.Name;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void StoreOrderCreation()
        {
            var store = new Store("Mello Mush", 954);
            var expected = store.CreateNewOrder("Jimmy");

            var actual = store.Cart;

            Assert.Equal(expected, actual);
        }
    }
}
