using System.Collections.Generic;
using System.Threading.Tasks;
using BurgerkingCaloriesCalculator.Domain.Models;

namespace BurgerkingCaloriesCalculator.Application.UseCases
{
    /// <summary>
    /// Creates a new menu
    /// </summary>
    public interface ICreateMenu
    {
        /// <summary>
        /// Creates a new menu from given products
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CreateMenuResponse> CreateMenu(CreateMenuRequest request);
    }

    public class CreateMenuRequest
    {
        public CreateMenuRequest(IEnumerable<string> productIds)
        {
            ProductIds = productIds;
        }

        public IEnumerable<string> ProductIds { get; }
    }

    public class CreateMenuResponse
    {
        public CreateMenuResponse(Menu menu, bool successful)
        {
            Menu = menu;
            Successful = successful;
        }

        public Menu Menu { get; }
        public bool Successful { get; }
    }
}