using GildedRoseKata.AgeingBehavior;
using GildedRoseKata.QualityBehaviors;
using System.Collections.Generic;

namespace GildedRoseKata
{
    public class ProductBehaviorFactory
    {
        public ProductBehavior GetProductBehavior(string productName)
        {
            return productName switch
            {
                "Aged Brie" => new ProductBehavior(new IncreaseAgeBehavior(), new AdjustQualityBehavior(1)),
                "Backstage passes" => new ProductBehavior(new IncreaseAgeBehavior(), new AdvancedQualityBehavior(
                    defaultBehavior: new AdjustQualityBehavior(1),
                    behaviorPhases: new List<(IQualityBehavior, int)>
                    {
                        (new AdjustQualityBehavior(2), 10),
                        (new AdjustQualityBehavior(3), 5),
                        (new SetQualityBehavior(0), 0)
                    })),
                "Conjured" => new ProductBehavior(new IncreaseAgeBehavior(), new AdjustQualityBehavior(-2)),
                "Normal Item" => new ProductBehavior(new IncreaseAgeBehavior(), new AdjustQualityBehavior(-1)),
                "Sulfuras" => new ProductBehavior(new NullAgeingBehavior(), new NullQualityBehavior()),
                _ => null
            };
        }
    }
}
