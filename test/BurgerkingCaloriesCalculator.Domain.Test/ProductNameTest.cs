using BurgerkingCaloriesCalculator.Domain.Models;
using FluentAssertions;
using Xunit;

namespace BurgerkingCaloriesCalculator.Domain.Test
{
    public class ProductNameTest
    {
        [InlineData("")]
        [InlineData("product with name")]
        [Theory]
        public void ItShallCreateProductNameWithProvidedName(string name)
        {
            var product = ProductName.Create(name);
            product.Name.Should().Be(name);
        }
    }
}