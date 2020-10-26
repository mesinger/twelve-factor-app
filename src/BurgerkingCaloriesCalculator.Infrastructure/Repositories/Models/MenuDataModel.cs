using System.Collections.Generic;

namespace BurgerkingCaloriesCalculator.Infrastructure.Repositories.Models
{
    internal class MenuDataModel
    {
        public string Id { get; set; }
        public IEnumerable<string> ProductIds { get; set; }
    }
}