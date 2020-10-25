using System.Collections.Generic;
using Newtonsoft.Json;

namespace BurgerkingCaloriesCalculator.Infrastructure.Repositories.Dto
{
    internal class ProductResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        [JsonProperty("nutritionfacts")]
        public IEnumerable<ProductResponseNutritionFact> NutritionFacts { get; set; }
    }

    internal class ProductResponseNutritionFact
    {
        public string Key { get; set; }
        public double Value { get; set; }
    }
}