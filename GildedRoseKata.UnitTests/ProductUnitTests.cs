using FluentAssertions;
using Xunit;

namespace GildedRoseKata.UnitTests
{
    public class ProductUnitTests
    {
        [Fact]
        void ConstructorSetsNameProperty()
        {
            var productName = "Test Product";
            var product = new Product(productName, 0, 0);

            product.Name.Should().Be(productName);
        }

        [Fact]
        void ConstructorSetsSellInProperty()
        {
            var sellIn = 10;
            var product = new Product("", sellIn, 0);

            product.SellIn.Should().Be(sellIn);
        }

        [Fact]
        void ConstructorSetsQualityProperty()
        {
            var quality = 10;
            var product = new Product("", 0, quality);

            product.Quality.Should().Be(quality);
        }

        [Fact]
        void SettingQualityBelow0SetsItAs0()
        {
            var initialQuality = -10;
            var product = new Product("", 0, initialQuality);

            product.Quality.Should().Be(Product.QualityMinimumValue);
        }

        [Fact]
        void SettingQualityBelow0SetsItAs0_AdjustmentCheck()
        {
            // We want to be sure that the internal value is set as 0, so we make an
            // adjustment and see if that adjustment is applied to 0, and not the 
            // value it was given.

            var initialQuality = -10;
            var adjustment = 5;

            var product = new Product("", 0, initialQuality);
           
            product.ChangeQualityBy(adjustment);

            product.Quality.Should().Be(adjustment);
        }
        
        [Fact]
        void QualitySetHigherThanMaxReportedValueInternallySetAsGivenValue()
        {
            // We want to be sure that the internal value is set as the given value, so we
            // make an adjustment and see if that adjustment is applied to the given value,
            // and not the max reported value.

            var initialQuality = 55;
            var adjustment = -10;
            var expctectedQualityAfterAdjustment = 45; // Would be 40 if internal value is set to max reported value
            
            var product = new Product("", 0, initialQuality);
           
            product.ChangeQualityBy(adjustment);

            product.Quality.Should().Be(expctectedQualityAfterAdjustment);
        }

        [Fact]
        void QualityReturnsMaxReportedValueIfIsGreaterThanMaxReportedValue()
        {
            var initialQuality = 55;
            var product = new Product("", 0, initialQuality);

            product.Quality.Should().Be(Product.QualityMaximumReportedValue);
        }
    }
}
