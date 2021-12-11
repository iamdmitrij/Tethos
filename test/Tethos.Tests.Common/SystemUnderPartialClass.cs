namespace Tethos.Tests.Common
{
    public class SystemUnderPartialClass
    {
        public SystemUnderPartialClass(PartialThreshold threshold)
        {
            this.Threshold = threshold;
        }

        public PartialThreshold Threshold { get; }

        public int Exercise() => this.Threshold.Enabled ? 1 : 0;
    }
}
