namespace BurgerkingCaloriesCalculator.Domain.Models
{
    /// <summary>
    /// Represents the name <see cref="Product"/>
    /// </summary>
    public class ProductName
    {
        protected ProductName(string name)
        {
            Name = name;
        }
        
        public static ProductName Create(string name) => new ProductName(name);

        public string Name { get; }
    }
}