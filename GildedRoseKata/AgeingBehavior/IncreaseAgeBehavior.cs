namespace GildedRoseKata.AgeingBehavior
{
    public class IncreaseAgeBehavior : IAgeingBehavior
    {
        public void ChangeAge(Product product)
        {
            product.SellIn--;
        }
    }
}
