using System;
using System.Collections.Generic;

namespace GildedRoseKata.QualityBehaviors
{
    public class AdvancedQualityBehavior : IQualityBehavior
    {
        private readonly IQualityBehavior _defaultBehavior;
        private readonly List<(IQualityBehavior Behavior, int MaxSellInApplicable)> _behaviorPhases;

        public AdvancedQualityBehavior(IQualityBehavior defaultBehavior, List<(IQualityBehavior, int)> behaviorPhases)
        {
            _defaultBehavior = defaultBehavior ?? throw new ArgumentNullException(nameof(defaultBehavior));
            _behaviorPhases = behaviorPhases ?? new List<(IQualityBehavior, int)>();
            _behaviorPhases.Sort((a, b) => b.MaxSellInApplicable.CompareTo(a.MaxSellInApplicable));
        }

        public void ChangeQuality(Product product)
        {
            var chosenBehavior = _defaultBehavior;
            foreach (var behaviorAndAgeRange in _behaviorPhases)
            {
                if (product.SellIn <= behaviorAndAgeRange.MaxSellInApplicable)
                {
                    chosenBehavior = behaviorAndAgeRange.Behavior;
                }
            }

            chosenBehavior.ChangeQuality(product);

        }
    }
}
