namespace Tethos.Tests.Common
{
    public class SystemUnderPartialClass
    {
        public PartialThreshold Threshold { get; }

        public SystemUnderPartialClass(PartialThreshold threshold)
        {
            Threshold = threshold;
        }

        public int Do() => Threshold.Enalbed ? 1 : 0;
    }
}
