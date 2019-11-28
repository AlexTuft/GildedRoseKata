using GildedRoseKata.AgeingBehavior;
using GildedRoseKata.QualityBehaviors;
using System;

namespace GildedRoseKata
{
    public class Product
    {
        public const int QualityMinimumValue = 0;
        public const int QualityMaximumReportedValue = 50;

        private int _quality;

        public Product(string name, int sellIn, int quality)
        {   
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
    }
}
