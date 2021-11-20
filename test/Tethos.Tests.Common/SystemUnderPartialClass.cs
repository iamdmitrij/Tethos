namespace Tethos.Tests.Common
{
    public class SystemUnderPartialClass
    {
        public PartialThreshold Threshold { get; }

        public SystemUnderPartialClass(PartialThreshold threshold)
        {
            this.Threshold = threshold;
        }

        public int Do() => this.Threshold.Enalbed ? 1 : 0;
    }
}
