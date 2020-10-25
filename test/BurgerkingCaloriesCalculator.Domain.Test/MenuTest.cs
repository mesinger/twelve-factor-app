using System;
using System.Collections.Generic;
using System.Linq;
using BurgerkingCaloriesCalculator.Domain.Models;
using FluentAssertions;
using Xunit;

namespace BurgerkingCaloriesCalculator.Domain.Test
{
    public class MenuTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ItShallThrowOnEmptyId(string id)
        {
            Action action = () => Menu.Create(id,
                new[] {Product.Create("1234", ProductName.Create("name"), EnergyValue.CreateFromKj(1000)),});

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ItShallThrowOnEmptyProductsList()
        {
            Action action1 = () => Menu.Create("1234", Enumerable.Empty<Product>());
            action1.Should().Throw<ArgumentException>();
            
            Action action2 = () => Menu.Create(Enumerable.Empty<Product>());
            action2.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ItShallCreateIdIfNotProvided()
        {
            var menu = Menu.Create(new[]
                {Product.Create("1234", ProductName.Create("name"), EnergyValue.CreateFromKj(1000)),});

            menu.Id.Should().NotBeNullOrWhiteSpace();
        }
        
        [Fact]
        public void ItShallCalculateMenuEnergyValue()
        {
            var products = new[]
            {
                Product.Create("1234", ProductName.Create("name"), EnergyValue.CreateFromKj(1000)),
                Product.Create("5678", ProductName.Create("name"), EnergyValue.CreateFromKCal(500)),
                Product.Create("9123", ProductName.Create("name"), EnergyValue.CreateFromKCal(245)),
                Product.Create("9123", ProductName.Create("name"), EnergyValue.CreateFromKj(764)),
            };

            var menu = Menu.Create(products);

            var menuEnergyValue = menu.CalculateMenuEnergyValue();

            var expectedMenuKj = (from product in products select product.Energy.ValueInKj).Sum();
            var expectedMenuKCal = (from product in products select product.Energy.ValueInKCal).Sum();

            menuEnergyValue.ValueInKj.Should().BeApproximately(expectedMenuKj, 0.0001);
            menuEnergyValue.ValueInKCal.Should().BeApproximately(expectedMenuKCal, 0.0001);
        }
    }
}