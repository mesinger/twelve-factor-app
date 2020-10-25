using System.Collections.Generic;
using System.Threading.Tasks;
using BurgerkingCaloriesCalculator.Domain.Models;
using CSharpFunctionalExtensions;

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

        /// <summary>
        /// Finds a product with a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Maybe<Product>> FindById(string id);

        /// <summary>
        /// Finds all products with a given id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IEnumerable<Product>> FindAllById(IEnumerable<string> ids);
    }
}