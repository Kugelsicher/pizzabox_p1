using System;
using System.Collections.Generic;
using PizzaBox.Domain.Models;
using Xunit;

namespace PizzaBox.Testing.Tests
{
    public class PrebuiltPizzaTests
    {
        [Fact]
        public void PersonalPriceCheck()
        {
            var prebuiltPrice = 0.50m;
            var expected = prebuiltPrice + 5m;
            var pizza = new PrebuiltPizza("Personal Plain", "Hand Tossed", "Personal", new List<string>(), prebuiltPrice, 1492);

            var actual = pizza.GetPrice();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MediumPriceCheck()
        {
            var prebuiltPrice = 1.50m;
            var expected = prebuiltPrice + 9m;
            var pizza = new PrebuiltPizza("Medium Plain", "Hand Tossed", "Medium", new List<string>(), prebuiltPrice, 4125);

            var actual = pizza.GetPrice();

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void LargePriceCheck()
        {
            var prebuiltPrice = 2.00m;
            var expected = prebuiltPrice + 12m;
            var pizza = new PrebuiltPizza("Personal Plain", "Hand Tossed", "Personal", new List<string>(), prebuiltPrice, 8354);

            var actual = pizza.GetPrice();

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void XLPriceCheck()
        {
            var prebuiltPrice = 2.50m;
            var expected = prebuiltPrice + 15m;
            var pizza = new PrebuiltPizza("Personal Plain", "Hand Tossed", "Personal", new List<string>(), prebuiltPrice, 1497522);

            var actual = pizza.GetPrice();

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void CheeseCrustPriceCheck()
        {
            var prebuiltPrice = 4.00m;
            var expected = prebuiltPrice + 5m;
            var pizza = new PrebuiltPizza("Personal Plain", "Hand Tossed", "Personal", new List<string>(), prebuiltPrice, 73857);

            var actual = pizza.GetPrice();

            Assert.Equal(expected, actual);
        }

    }
}
