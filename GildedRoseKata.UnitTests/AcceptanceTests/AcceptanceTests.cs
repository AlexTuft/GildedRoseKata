using FluentAssertions;
using Xunit;

namespace GildedRoseKata.UnitTests.AcceptanceTests
{
    public class AcceptanceTests
    {
        [Fact]
        void AgedBrie()
        {
            AgeItem(new Product("Aged Brie", 1, 1),
                new Product("Aged Brie", 0, 2));
        }

        [Fact]
        void BackstagePasses()
        {
            AgeItem(new Product("Backstage passes", -1, 2),
                new Product("Backstage passes", -2, 0));
        }

        [Fact]
        void BackstagePasses_2()
        {
            AgeItem(new Product("Backstage passes", 9, 2),
                new Product("Backstage passes", 8, 4));
        }

        [Fact]
        void Sulfuras()
        {
            AgeItem(new Product("Sulfuras", 2, 2),
                new Product("Sulfuras", 2, 2));
        }

        [Fact]
        void NormalItem()
        {
            AgeItem(new Product("Normal Item", -1, 55),
                new Product("Normal Item", -2, 50));
        }

        [Fact]
        void NormalItem_2()
        {
            AgeItem(new Product("Normal Item", 2, 2),
                new Product("Normal Item", 1, 1));
        }

        [Fact]
        void InvalidItem()
        {
            AgeItem(new Product("INVALID ITEM", 2, 2),
                null);
        }

        [Fact]
        void Conjured()
        {
            AgeItem(new Product("Conjured", 2, 2),
                new Product("Conjured", 1, 0));
        }

        [Fact]
        void Conjured_2()
        {
            AgeItem(new Product("Conjured", -1, 5),
                new Product("Conjured", -2, 1));
        }

        void AgeItem(Product product, Product expected)
        {
            var productFactory = new ProductBehaviorFactory();
            var behavior = productFactory.GetProductBehavior(product.Name);
            if (behavior != null)
            {
                behavior.ElapseDay(product);
                product.SellIn.Should().Be(expected.SellIn);
                product.Quality.Should().Be(expected.Quality);
            }
        }
    }
}
