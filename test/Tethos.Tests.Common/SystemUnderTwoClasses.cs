namespace Tethos.Tests.Common
{
    public class SystemUnderTwoClasses
    {
        public SystemUnderTwoClasses(Concrete mockable, Threshold threshold)
        {
            this.Mockable = mockable;
            this.Threshold = threshold;
        }

        public Concrete Mockable { get; }

        public Threshold Threshold { get; }

        public int Do() => this.Threshold.Enalbed ? this.Mockable.Do() : 0;
    }
}
