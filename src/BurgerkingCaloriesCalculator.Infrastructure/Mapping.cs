using System.Linq;
using BurgerkingCaloriesCalculator.Domain.Models;
using BurgerkingCaloriesCalculator.Infrastructure.Repositories.Models;

namespace BurgerkingCaloriesCalculator.Infrastructure
{
    internal static class Mapping
    {
        public static MenuDataModel ToData(this Menu menu)
        {
            return new MenuDataModel
            {
                Id = menu.Id,
                ProductIds = from product in menu.MenuProducts select product.Id
            };
        }
    }
}