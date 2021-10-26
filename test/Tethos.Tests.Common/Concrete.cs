using static PeanutButter.RandomGenerators.RandomValueGen;

namespace Tethos.Tests.Common
{
    public class Concrete : IMockable
    {
        public int Do() => GetRandomInt(0, 10);
    }
}
