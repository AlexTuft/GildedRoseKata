using FluentAssertions;
using GildedRoseKata.QualityBehaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GildedRoseKata.UnitTests
{
    public class AdvancedQualityBehaviorTests
    {
        private readonly List<(IQualityBehavior, int)> _behaviorPhases = new List<(IQualityBehavior, int)>
        {
            (new AdjustQualityBehavior(2), 10),
            (new AdjustQualityBehavior(3), 5),
            (new SetQualityBehavior(0), 0)
        };

        [Theory]
        [InlineData(10, 12)]
        [InlineData(6, 12)]
        [InlineData(5, 13)]
        [InlineData(0, 0)]
        void ChangesQualityByRateWithSmallestAssociatedAgeThatIsGreaterThanOrEqualToSellInValue(int sellIn, int expectedQuality)
        {
            Assert.True(sellIn <= 10);

            var product = new Product("", sellIn, 10);
            var qualityBehavior = new AdvancedQualityBehavior(new NullQualityBehavior(), _behaviorPhases);

            qualityBehavior.ChangeQuality(product);

            product.Quality.Should().Be(expectedQuality);
        }

        [Theory]
        [InlineData(10, 12)]
        [InlineData(6, 12)]
        [InlineData(5, 13)]
        [InlineData(0, 0)]
        void OrderOfItemsInBehaviorPhasesDoesNotAffectChosenPhase(int sellIn, int expectedQuality)
        {
            Assert.True(sellIn <= 10);

            var behaviorPhasesRandomized = _behaviorPhases.OrderBy(_ => Guid.NewGuid()).ToList();

            var product = new Product("", sellIn, 10);
            var qualityBehavior = new AdvancedQualityBehavior(new NullQualityBehavior(), behaviorPhasesRandomized);

            qualityBehavior.ChangeQuality(product);

            product.Quality.Should().Be(expectedQuality);
        }

        [Fact]
        void UsesDefaultBehaviorIfSellInValueIsGreaterThanAnyPhaseApplicableRange()
        {
            var defaultBehavior = new AdjustQualityBehavior(1);
            var behaviorPhases = new List<(IQualityBehavior behavior, int MaxSellInApplicable)>
            {
                (new AdjustQualityBehavior(3), 5),
                (new SetQualityBehavior(0), 0),
                (new AdjustQualityBehavior(2), 10)
            };

            var product = new Product("", 11, 10);
            var qualityBehavior = new AdvancedQualityBehavior(defaultBehavior, behaviorPhases);

            qualityBehavior.ChangeQuality(product);

            product.Quality.Should().Be(11);
        }

        [Fact]
        void ThrowsExceptionIfDefaultBehavoirIsNull()
        {
            Action act = () => new AdvancedQualityBehavior(defaultBehavior: null, new List<(IQualityBehavior, int)>());

            act.Should().Throw<ArgumentNullException>();
        }
    }
}
