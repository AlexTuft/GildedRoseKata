using FluentAssertions;
using GildedRoseKata.QualityBehaviors;
using Xunit;

namespace GildedRoseKata.UnitTests
{
    public class AdjustQualityBehaviorTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(-1)]
        [InlineData(-2)]
        void ShouldChangeQualityByGivenRate_WhenSellInIsAbove0(int changeRate)
        {
            int initialQuality = 10;
            int expectedQualityAfterChange = initialQuality + changeRate;

            var product = new Product("", 1, initialQuality);
            var qualityBehavior = new AdjustQualityBehavior(changeRate);

            qualityBehavior.ChangeQuality(product);

            product.Quality.Should().Be(expectedQualityAfterChange);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(-1)]
        [InlineData(-2)]
        void ShouldChangeQualityTwiceAsFast_WhenSellInIsLessThanOrEqualTo0(int changeRate)
        {
            int initialQuality = 10;
            int expectedQualityAfterChange = initialQuality + (changeRate * 2);

            var product = new Product("", -1, initialQuality);
            var qualityBehavior = new AdjustQualityBehavior(changeRate);

            qualityBehavior.ChangeQuality(product);

            product.Quality.Should().Be(expectedQualityAfterChange);
        }
    }
}
