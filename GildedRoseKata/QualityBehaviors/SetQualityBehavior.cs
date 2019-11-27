namespace GildedRoseKata.QualityBehaviors
{
    public class SetQualityBehavior : IQualityBehavior
    {
        private readonly int _setTo;

        public SetQualityBehavior(int setTo)
        {
            _setTo = setTo;
        }

        public void ChangeQuality(Product product)
        {
            product.Quality = _setTo;
        }
    }
}
