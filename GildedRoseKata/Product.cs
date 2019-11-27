using GildedRoseKata.AgeingBehavior;
using GildedRoseKata.QualityBehaviors;
using System;

namespace GildedRoseKata
{
    public class Product
    {
        public const int QualityMinimumValue = 0;
        public const int QualityMaximumReportedValue = 50;

        private readonly IAgeingBehavior _agingBehavior;
        private readonly IQualityBehavior _qualityBehavior;
        
        private int _quality;

        public Product(string name, int sellIn, int quality, IAgeingBehavior agingBehavior, IQualityBehavior qualityBehavior)
        {
            _agingBehavior = agingBehavior ?? throw new ArgumentNullException(nameof(agingBehavior));
            _qualityBehavior = qualityBehavior ?? throw new ArgumentNullException(nameof(qualityBehavior));

            Name = name;
            SellIn = sellIn;
            Quality = quality;
        }

        public string Name { get; }

        public int SellIn { get; set; }
        
        public int Quality {
            get => Math.Min(_quality, QualityMaximumReportedValue);
            set
            {
                _quality = Math.Max(value, QualityMinimumValue);
            }
        }

        public void ChangeQualityBy(int changeAmount)
        {
            Quality = _quality + changeAmount;
        }

        public void ElapseDay()
        {
            _qualityBehavior.ChangeQuality(this);
            _agingBehavior.ChangeAge(this);
        }
    }
}
