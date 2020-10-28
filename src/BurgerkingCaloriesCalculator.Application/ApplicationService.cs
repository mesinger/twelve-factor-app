using System.Collections.Generic;
using System.Threading.Tasks;
using BurgerkingCaloriesCalculator.Application.UseCases;
using BurgerkingCaloriesCalculator.Domain.Models;
using BurgerkingCaloriesCalculator.Domain.Repositories;

namespace BurgerkingCaloriesCalculator.Application
{
    /// <summary>
    /// Application service: Implementation for application layer use cases
    /// </summary>
    public class ApplicationService : IFindAllProducts, ICreateMenu, IFindAllMenus
    {
        private readonly IProductRepository _productRepository;
        private readonly IMenuRepository _menuRepository;

        public ApplicationService(IProductRepository productRepository, IMenuRepository menuRepository)
        {
            _productRepository = productRepository;
            _menuRepository = menuRepository;
        }
        
        /// <inheritdoc />
        public async Task<IEnumerable<Product>> FindAllProducts()
        {
            var products = await _productRepository.FindAll();
            return products;
        }

        /// <inheritdoc />
        public async Task<CreateMenuResponse> CreateMenu(CreateMenuRequest request)
        {
            var products = await _productRepository.FindAllById(request.ProductIds);
            var menu = Menu.Create(products);
            await _menuRepository.Save(menu);
            return new CreateMenuResponse(menu, true);
        }

        /// <inheritdoc />
        public Task<IEnumerable<Menu>> FindAllMenus()
        {
            return _menuRepository.FindAll();
        }
    }
}