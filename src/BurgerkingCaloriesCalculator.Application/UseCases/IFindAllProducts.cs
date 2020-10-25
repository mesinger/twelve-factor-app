using System.Collections.Generic;
using System.Threading.Tasks;
using BurgerkingCaloriesCalculator.Domain.Models;

namespace BurgerkingCaloriesCalculator.Application.UseCases
{
    /// <summary>
    /// Finds all products
    /// </summary>
    public interface IFindAllProducts
    {
        /// <summary>
        /// Finds all products
        /// </summary>
        /// <returns></returns>
        Task<FindAllProductsResponse> FindAll();
    }

    public class FindAllProductsResponse
    {
        public FindAllProductsResponse(IEnumerable<Product> products)
        {
            Products = products;
        }

        public IEnumerable<Product> Products { get; }
    }
}