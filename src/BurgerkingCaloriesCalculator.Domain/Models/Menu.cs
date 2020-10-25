using System;
using System.Collections.Generic;
using System.Linq;

namespace BurgerkingCaloriesCalculator.Domain.Models
{
    /// <summary>
    /// Aggregate root for a menu, that consists of a number of <see cref="Product"/>s.
    /// These products make the total energy value of a menu.
    /// </summary>
    public class Menu
    {
        protected Menu(string id, IEnumerable<Product> menuProducts)
        {
            Id = id;
            MenuProducts = menuProducts;
        }

        public static Menu Create(string id, IEnumerable<Product> menuProducts)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Unable to create menu with empty id");
            }

            var products = menuProducts.ToList();
            if (!products.Any())
            {
                throw new ArgumentException("Unable to create menu with 0 products");
            }
            
            return new Menu(id, products);
        }
        
        public static Menu Create(IEnumerable<Product> menuProducts)
        {
            var products = menuProducts.ToList();
            if (!products.Any())
            {
                throw new ArgumentException("Unable to create menu with 0 products");
            }
            
            return new Menu(Guid.NewGuid().ToString(), products);
        }

        /// <summary>
        /// The identifier for this entity
        /// </summary>
        public string Id { get; }
        
        /// <summary>
        /// The products this menu consists of
        /// </summary>
        public IEnumerable<Product> MenuProducts { get; }

        public EnergyValue CalculateMenuEnergyValue()
        {
            var totalEnergyInKj = (from product in MenuProducts select product.Energy.ValueInKj).Sum();
            return EnergyValue.CreateFromKj(totalEnergyInKj);
        }
    }
}