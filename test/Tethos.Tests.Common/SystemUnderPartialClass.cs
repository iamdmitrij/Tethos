namespace Tethos.Tests.Common
{
    public class SystemUnderPartialClass
    {
        public SystemUnderPartialClass(PartialThreshold threshold)
        {
            this.Threshold = threshold;
        }

        public PartialThreshold Threshold { get; }

        public int Do() => this.Threshold.Enalbed ? 1 : 0;
    }
}
