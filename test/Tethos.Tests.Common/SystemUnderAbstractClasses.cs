namespace Tethos.Tests.Common
{
    public class SystemUnderAbstractClasses
    {
        public SystemUnderAbstractClasses(AbstractThreshold threshold)
        {
            this.Threshold = threshold;
        }

        public AbstractThreshold Threshold { get; }

        public int Do() => this.Threshold.Enalbed ? 1 : 0;
    }
}
