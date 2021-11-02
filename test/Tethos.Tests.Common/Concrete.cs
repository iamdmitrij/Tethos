namespace Tethos.Tests.Common
{
    using static PeanutButter.RandomGenerators.RandomValueGen;

    public class Concrete : IMockable
    {
        public int Do() => GetRandomInt(0, 10);
    }
}
