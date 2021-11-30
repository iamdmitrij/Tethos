namespace Tethos.Tests.Common
{
    using static PeanutButter.RandomGenerators.RandomValueGen;

    public class Concrete : IMockable
    {
        public Concrete()
            : this(0, 10)
        {
        }

        public Concrete(int minValue, int maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        public virtual int Get() => GetRandomInt(this.MinValue, this.MaxValue);
    }
}
