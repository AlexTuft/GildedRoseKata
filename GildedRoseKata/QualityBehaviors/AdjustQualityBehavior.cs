namespace GildedRoseKata.QualityBehaviors
{
    public class AdjustQualityBehavior : IQualityBehavior
    {
        private readonly int _changeRate;

        public AdjustQualityBehavior(int changeRate)
        {
            _changeRate = changeRate;
        }

        public void ChangeQuality(Product product)
        {
            if (product.SellIn > 0)
            {
                product.ChangeQualityBy(_changeRate);
            }
            else
            {
                product.ChangeQualityBy(_changeRate * 2);
            }
        }
    }
}
