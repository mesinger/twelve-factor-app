using System.Collections.Generic;
using System.Threading.Tasks;
using BurgerkingCaloriesCalculator.Domain.Models;

namespace BurgerkingCaloriesCalculator.Domain.Repositories
{
    /// <summary>
    /// Access point for <see cref="Menu"/> entities
    /// </summary>
    public interface IMenuRepository
    {
        /// <summary>
        /// Persists a new menu
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        Task Save(Menu menu);

        /// <summary>
        /// Finds all <see cref="Menu"/> entities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Menu>> FindAll();
    }
}