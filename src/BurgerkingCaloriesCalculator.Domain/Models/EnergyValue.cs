using System;

namespace BurgerkingCaloriesCalculator.Domain.Models
{
    /// <summary>
    /// Represents a energy value of food
    /// </summary>
    public class EnergyValue
    {
        protected EnergyValue(double valueInKj, double valueInKCal)
        {
            ValueInKj = valueInKj;
            ValueInKCal = valueInKCal;
        }

        public static EnergyValue CreateFromKj(double valueInKj)
        {
            if (valueInKj < 0)
            {
                throw new ArgumentException($"Unable to create energy value from negative ({valueInKj}) KJ.");
            }

            var valueInKCal = valueInKj / 4.184;
            
            return new EnergyValue(valueInKj, valueInKCal);
        }
        
        public static EnergyValue CreateFromKCal(double valueInKCal)
        {
            if (valueInKCal < 0)
            {
                throw new ArgumentException($"Unable to create energy value from negative ({valueInKCal}) KCal.");
            }

            var valueInKj = valueInKCal * 4.184;
            
            return new EnergyValue(valueInKj, valueInKCal);
        }

        public double ValueInKj { get; }
        public double ValueInKCal { get; }
    }
}