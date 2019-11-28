using FluentAssertions;
using GildedRoseKata.AgeingBehavior;
using Xunit;

namespace GildedRoseKata.UnitTests
{
    public class DefaultAgingBehaviorTests
    {
        [Fact]
        void ChangedProductAgeBy1()
        {
            int initialSellIn = 10;

            var product = new Product("", initialSellIn, 0);
            var ageingBehavior = new IncreaseAgeBehavior();

            ageingBehavior.ChangeAge(product);

            product.SellIn.Should().Be(initialSellIn - 1);
        }
    }
}
