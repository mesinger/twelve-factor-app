using System.Collections.Generic;
using System.Threading.Tasks;
using BurgerkingCaloriesCalculator.Domain.Models;

namespace BurgerkingCaloriesCalculator.Application.UseCases
{
    /// <summary>
    /// Finds all <see cref="Menu"/>s
    /// </summary>
    public interface IFindAllMenus
    {
        /// <summary>
        /// Finds all <see cref="Menu"/>s
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Menu>> FindAllMenus();
    }
}