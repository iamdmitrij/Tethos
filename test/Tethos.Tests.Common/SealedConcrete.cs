namespace Tethos.Tests.Common
{
    public sealed class SealedConcrete : Concrete
    {
        public SealedConcrete() : base()
        {
        }

        public SealedConcrete(int minValue, int maxValue) : base(minValue, maxValue)
        {
        }
    }
}
