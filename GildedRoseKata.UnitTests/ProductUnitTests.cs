using FluentAssertions;
using GildedRoseKata.AgeingBehavior;
using GildedRoseKata.QualityBehaviors;
using Moq;
using System;
using Xunit;

namespace GildedRoseKata.UnitTests
{
    public class ProductUnitTests
    {
        [Fact]
        void ConstructorSetsNameProperty()
        {
            var productName = "Test Product";
            var product = new Product(productName, 0, 0, new NullAgeingBehavior(), new NullQualityBehavior());

            product.Name.Should().Be(productName);
        }

        [Fact]
        void ConstructorSetsSellInProperty()
        {
            var sellIn = 10;
            var product = new Product("", sellIn, 0, new NullAgeingBehavior(), new NullQualityBehavior());

            product.SellIn.Should().Be(sellIn);
        }

        [Fact]
        void ConstructorSetsQualityProperty()
        {
            var quality = 10;
            var product = new Product("", 0, quality, new NullAgeingBehavior(), new NullQualityBehavior());

            product.Quality.Should().Be(quality);
        }

        [Fact]
        void SettingQualityBelow0SetsItAs0()
        {
            var initialQuality = -10;
            var product = new Product("", 0, initialQuality, new NullAgeingBehavior(), new NullQualityBehavior());

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

            var product = new Product("", 0, initialQuality, new NullAgeingBehavior(), new NullQualityBehavior());
           
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
            
            var product = new Product("", 0, initialQuality, new NullAgeingBehavior(), new NullQualityBehavior());
           
            product.ChangeQualityBy(adjustment);

            product.Quality.Should().Be(expctectedQualityAfterAdjustment);
        }

        [Fact]
        void QualityReturnsMaxReportedValueIfIsGreaterThanMaxReportedValue()
        {
            var initialQuality = 55;
            var product = new Product("", 0, initialQuality, new NullAgeingBehavior(), new NullQualityBehavior());

            product.Quality.Should().Be(Product.QualityMaximumReportedValue);
        }

        [Fact]
        void ThrowsExceptionIfAgingBehavoirIsNull()
        {
            Action act = () => new Product("", 0, 0, null, new NullQualityBehavior());

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        void ThrowsExceptionIfQualityBehavoirIsNull()
        {
            Action act = () => new Product("", 0, 0, new NullAgeingBehavior(), null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        void ElapsingADayCallsQualityBehaviour()
        {
            var mockAgingBehavior = new Mock<IAgeingBehavior>();
            var product = new Product("", 0, 0, mockAgingBehavior.Object, new NullQualityBehavior());

            product.ElapseDay();

            mockAgingBehavior.Verify(m => m.ChangeAge(product), Times.Once);
        }

        [Fact]
        void ElapsingADayCallsAgingBehavior()
        {
            var mockQualityBehavior = new Mock<IQualityBehavior>();
            var product = new Product("", 0, 0, new NullAgeingBehavior(), mockQualityBehavior.Object);

            product.ElapseDay();

            mockQualityBehavior.Verify(m => m.ChangeQuality(product), Times.Once);
        }

        [Fact]
        void ElapsingADayCallsQualityBehvaiorBeforeAgingBehavior()
        {
            int callOrder = 0;

            var mockAgingBehavior = new Mock<IAgeingBehavior>();
            mockAgingBehavior.Setup(m => m.ChangeAge(It.IsAny<Product>())).Callback(() => Assert.Equal(1, callOrder++));

            var mockQualityBehavior = new Mock<IQualityBehavior>();
            mockQualityBehavior.Setup(m => m.ChangeQuality(It.IsAny<Product>())).Callback(() => Assert.Equal(0, callOrder++));

            var product = new Product("", 0, 0, mockAgingBehavior.Object, mockQualityBehavior.Object);

            product.ElapseDay();
        }
    }
}
