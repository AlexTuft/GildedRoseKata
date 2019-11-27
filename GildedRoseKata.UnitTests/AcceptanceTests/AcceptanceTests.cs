using FluentAssertions;
using GildedRoseKata.AgeingBehavior;
using GildedRoseKata.QualityBehaviors;
using System.Collections.Generic;
using Xunit;

namespace GildedRoseKata.UnitTests.AcceptanceTests
{
    public class AcceptanceTests
    {
        [Fact]
        void AcceptanceTest1()
        {
            var productFactory = new ProductFactory();

            var products = new List<Product>
            {
                productFactory.GetProduct("Aged Brie", 1, 1),
                productFactory.GetProduct("Backstage passes", -1, 2),
                productFactory.GetProduct("Backstage passes", 9, 2),
                productFactory.GetProduct("Sulfuras", 2, 2),
                productFactory.GetProduct("Normal Item", -1, 55),
                productFactory.GetProduct("Normal Item", 2, 2),
                productFactory.GetProduct("INVALID ITEM", 2, 2),
                productFactory.GetProduct("Conjured", 2, 2),
                productFactory.GetProduct("Conjured", -1, 5)
            };

            var expected = new List<Product>
            {
               new Product("Aged Brie", 0, 2, new NullAgeingBehavior(), new NullQualityBehavior()),
                new Product("Backstage passes", -2, 0, new NullAgeingBehavior(), new NullQualityBehavior()),
                new Product("Backstage passes", 8, 4, new NullAgeingBehavior(), new NullQualityBehavior()),
                new Product("Sulfuras", 2, 2, new NullAgeingBehavior(), new NullQualityBehavior()),
                new Product("Normal Item", -2, 50, new NullAgeingBehavior(), new NullQualityBehavior()),
                new Product("Normal Item", 1, 1, new NullAgeingBehavior(), new NullQualityBehavior()),
                null,
                new Product("Conjured", 1, 0, new NullAgeingBehavior(), new NullQualityBehavior()),
                new Product("Conjured", -2, 1, new NullAgeingBehavior(), new NullQualityBehavior())
            };

            products.ForEach(p => p?.ElapseDay());

            products.Should().Equal(expected, (actual, expceted) =>
                actual?.SellIn == expceted?.SellIn && actual?.Quality == expceted?.Quality);
        }
    }
}
