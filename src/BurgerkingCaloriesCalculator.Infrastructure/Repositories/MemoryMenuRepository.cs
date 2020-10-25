using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerkingCaloriesCalculator.Domain.Models;
using BurgerkingCaloriesCalculator.Domain.Repositories;

namespace BurgerkingCaloriesCalculator.Infrastructure.Repositories
{
    /// <summary>
    /// An in memory implementation of a menu repository
    /// </summary>
    public class MemoryMenuRepository : IMenuRepository
    {
        private readonly HashSet<Menu> _menus;

        public MemoryMenuRepository()
        {
            _menus = new HashSet<Menu>();
        }
        
        /// <inheritdoc />
        public Task Save(Menu menu)
        {
            _menus.Add(menu);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<IEnumerable<Menu>> FindAll()
        {
            return Task.FromResult(_menus.ToList() as IEnumerable<Menu>);
        }
    }
}