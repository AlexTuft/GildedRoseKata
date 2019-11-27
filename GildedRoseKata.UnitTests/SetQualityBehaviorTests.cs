using FluentAssertions;
using GildedRoseKata.AgeingBehavior;
using GildedRoseKata.QualityBehaviors;
using Xunit;

namespace GildedRoseKata.UnitTests
{
    public class SetQualityBehaviorTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        void SetsQualityToGivenValue(int setTo)
        {
            int initialQuality = 10;

            var product = new Product("", 1, initialQuality, new NullAgeingBehavior(), new NullQualityBehavior());
            var qualityBehavior = new SetQualityBehavior(setTo);

            qualityBehavior.ChangeQuality(product);

            product.Quality.Should().Be(setTo);
        }

    }
}
