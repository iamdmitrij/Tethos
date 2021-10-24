namespace Tethos.Tests.Common
{
    public class SystemUnderTwoClasses
    {
        public Concrete Mockable { get; }

        public Threshold Threshold { get; }

        public SystemUnderTwoClasses(Concrete mockable, Threshold threshold)
        {
            Mockable = mockable;
            Threshold = threshold;
        }

        public int Do() => Threshold.Enalbed ? Mockable.Do() : 0;
    }
}
