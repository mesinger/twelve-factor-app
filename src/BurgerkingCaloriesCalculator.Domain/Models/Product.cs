namespace BurgerkingCaloriesCalculator.Domain.Models
{
    /// <summary>
    /// Represents a product
    /// </summary>
    public class Product
    {
        protected Product(string id, ProductName name, EnergyValue energy)
        {
            Id = id;
            Name = name;
            Energy = energy;
        }

        public static Product Create(string id, ProductName name, EnergyValue energy)
        {
            return new Product(id, name, energy);
        }

        /// <summary>
        /// A readonly identifier of this product
        /// </summary>
        public string Id { get; }
        
        /// <summary>
        /// The product's name
        /// </summary>
        public ProductName Name { get; }
        
        /// <summary>
        /// The products nutrition energy
        /// </summary>
        public EnergyValue Energy { get; }
    }
}