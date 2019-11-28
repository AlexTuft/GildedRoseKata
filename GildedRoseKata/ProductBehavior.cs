using GildedRoseKata.AgeingBehavior;
using GildedRoseKata.QualityBehaviors;
using System;

namespace GildedRoseKata
{
    public class ProductBehavior
    {
        private readonly IAgeingBehavior _agingBehavior;
        private readonly IQualityBehavior _qualityBehavior;
        
        public ProductBehavior(IAgeingBehavior agingBehavior, IQualityBehavior qualityBehavior)
        {
            _agingBehavior = agingBehavior ?? throw new ArgumentNullException(nameof(agingBehavior));
            _qualityBehavior = qualityBehavior ?? throw new ArgumentNullException(nameof(qualityBehavior));
        }

        public void ElapseDay(Product product)
        {
            _qualityBehavior.ChangeQuality(product);
            _agingBehavior.ChangeAge(product);
        }
    }
}
