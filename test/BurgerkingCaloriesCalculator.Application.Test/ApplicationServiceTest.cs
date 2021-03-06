using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerkingCaloriesCalculator.Application.UseCases;
using BurgerkingCaloriesCalculator.Domain.Models;
using BurgerkingCaloriesCalculator.Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace BurgerkingCaloriesCalculator.Application.Test
{
    public class ApplicationServiceTest
    {
        private readonly ApplicationService sut;
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Mock<IMenuRepository> _menuRepository;

        private static readonly IReadOnlyCollection<Product> Products = new[]
        {
            Product.Create("1234", ProductName.Create("name"), EnergyValue.CreateFromKj(1000)),
            Product.Create("5678", ProductName.Create("name"), EnergyValue.CreateFromKCal(500)),
            Product.Create("9123", ProductName.Create("name"), EnergyValue.CreateFromKCal(245)),
            Product.Create("9123", ProductName.Create("name"), EnergyValue.CreateFromKj(764)),
        };

        private static readonly IReadOnlyCollection<Menu> Menus = new[]
        {
            Menu.Create(Products),
            Menu.Create(Products),
            Menu.Create(Products),
        };

        public ApplicationServiceTest()
        {
            _productRepository = new Mock<IProductRepository>();
            _menuRepository = new Mock<IMenuRepository>();

            sut = new ApplicationService(_productRepository.Object, _menuRepository.Object);
        }

        [Fact]
        public async void ItShallFindAllProducts()
        {
            _productRepository.Setup(repo => repo.FindAll()).ReturnsAsync(Products);

            var products = await sut.FindAllProducts();

            products.Count().Should().Be(Products.Count);
        }
        
        [Fact]
        public async void ItShallFindAllMenus()
        {
            _menuRepository.Setup(repo => repo.FindAll()).ReturnsAsync(Menus);

            var menus = await sut.FindAllMenus();

            menus.Count().Should().Be(Menus.Count);
        }

        [Theory]
        [InlineData("1234")]
        [InlineData("1234", "5678", "9123")]
        public async void ItShallCreateMenu(params string[] productIds)
        {
            _productRepository.Setup(repo => repo.FindAllById(productIds)).ReturnsAsync(Products);

            var response = await sut.CreateMenu(new CreateMenuRequest(productIds));

            response.Menu.Should().NotBeNull();
            response.Successful.Should().BeTrue();
            _menuRepository.Verify(repo => repo.Save(It.IsAny<Menu>()), Times.Once);
        }

        [Fact]
        public void ItShallNotCreateMenuWithoutProducts()
        {
            var unknownIds = new[] {"1234", "5678"};
            _productRepository.Setup(repo => repo.FindAllById(unknownIds))
                .ReturnsAsync(Enumerable.Empty<Product>());

            Func<Task> action = async () => await sut.CreateMenu(new CreateMenuRequest(unknownIds));

            action.Should().Throw<ArgumentException>();
        }
    }
}