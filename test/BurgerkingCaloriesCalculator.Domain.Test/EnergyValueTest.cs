using System;
using BurgerkingCaloriesCalculator.Domain.Models;
using FluentAssertions;
using Xunit;

namespace BurgerkingCaloriesCalculator.Domain.Test
{
    public class EnergyValueTest
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(100, 100)]
        [InlineData(int.MaxValue, int.MaxValue)]
        public void ItShallCreateEnergyValues(double kj, double kCal)
        {
            var energyValueCalculatedFromKj = EnergyValue.CreateFromKj(kj);
            var energyValueCalculatedFromKCal = EnergyValue.CreateFromKCal(kCal);

            energyValueCalculatedFromKj.ValueInKj.Should().Be(kj);
            energyValueCalculatedFromKj.ValueInKCal.Should().Be(kj / 4.184);
            
            energyValueCalculatedFromKCal.ValueInKCal.Should().Be(kCal);
            energyValueCalculatedFromKCal.ValueInKj.Should().Be(kCal * 4.184);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-int.MaxValue)]
        public void ItShallThrowWhenCreatingEnergyWithNegativeKj(double kj)
        {
            Action action = () => EnergyValue.CreateFromKj(kj);
            action.Should().Throw<ArgumentException>();
        }
        
        [Theory]
        [InlineData(-1)]
        [InlineData(-int.MaxValue)]
        public void ItShallThrowWhenCreatingEnergyWithNegativeKCal(double kCal)
        {
            Action action = () => EnergyValue.CreateFromKCal(kCal);
            action.Should().Throw<ArgumentException>();
        }
    }
}