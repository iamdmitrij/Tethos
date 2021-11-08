using static PeanutButter.RandomGenerators.RandomValueGen;

namespace Tethos.Tests.Common
{
    public class Concrete : IMockable
    {
        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        public Concrete() : this(0, 10)
        {
        }

        public Concrete(int minValue, int maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public virtual int Do() => GetRandomInt(MinValue, MaxValue);
    }
}
