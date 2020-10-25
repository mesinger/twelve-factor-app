using System.Collections.Generic;
using System.Threading.Tasks;
using BurgerkingCaloriesCalculator.Domain.Models;

namespace BurgerkingCaloriesCalculator.Domain.Repositories
{
    /// <summary>
    /// Access to <see cref="Product"/>
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Finds all objects for <see cref="Product"/>
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Product>> FindAll();
    }
}