using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using BurgerkingCaloriesCalculator.Domain.Models;
using BurgerkingCaloriesCalculator.Domain.Repositories;
using BurgerkingCaloriesCalculator.Infrastructure.Context;
using BurgerkingCaloriesCalculator.Infrastructure.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerkingCaloriesCalculator.Infrastructure.Repositories
{
    /// <summary>
    /// Implementation of <see cref="IMenuRepository"/> with a rdbs
    /// </summary>
    public class MenuRepository : IMenuRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IProductRepository _productRepository;

        public MenuRepository(ApplicationDbContext dbContext, IProductRepository productRepository)
        {
            _dbContext = dbContext;
            _productRepository = productRepository;
        }
        
        /// <inheritdoc />
        public async Task Save(Menu menu)
        {
            await _dbContext.Menus.AddAsync(menu.ToData());
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Menu>> FindAll()
        {
            var menus = await _dbContext.Menus.ToListAsync();
            
            var requiredProductIds = menus.SelectMany(menu => menu.ProductIds);
            var requiredProducts = (await _productRepository.FindAllById(requiredProductIds)).ToImmutableList();

            return menus.Select(menu => Menu.Create(menu.Id, requiredProducts.Where(p => menu.ProductIds.Contains(p.Id)))).ToList();
        }
    }
}