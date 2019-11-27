using GildedRoseKata.AgeingBehavior;
using GildedRoseKata.QualityBehaviors;
using System.Collections.Generic;

namespace GildedRoseKata
{
    public class ProductFactory
    {
        public Product GetProduct(string productName, int sellIn, int quality)
        {
            return productName switch
            {
                "Aged Brie" => new Product(productName, sellIn, quality, new IncreaseAgeBehavior(), new AdjustQualityBehavior(1)),
                "Backstage passes" => new Product(productName, sellIn, quality, new IncreaseAgeBehavior(), new AdvancedQualityBehavior(
                    defaultBehavior: new AdjustQualityBehavior(1),
                    behaviorPhases: new List<(IQualityBehavior, int)>
                    {
                        (new AdjustQualityBehavior(2), 10),
                        (new AdjustQualityBehavior(3), 5),
                        (new SetQualityBehavior(0), 0)
                    })),
                "Conjured" => new Product(productName, sellIn, quality, new IncreaseAgeBehavior(), new AdjustQualityBehavior(-2)),
                "Normal Item" => new Product(productName, sellIn, quality, new IncreaseAgeBehavior(), new AdjustQualityBehavior(-1)),
                "Sulfuras" => new Product(productName, sellIn, quality, new NullAgeingBehavior(), new NullQualityBehavior()),
                _ => null
            };
        }
    }
}
