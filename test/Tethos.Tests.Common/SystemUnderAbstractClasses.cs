namespace Tethos.Tests.Common
{
    public class SystemUnderAbstractClasses
    {
        public AbstractThreshold Threshold { get; }

        public SystemUnderAbstractClasses(AbstractThreshold threshold)
        {
            this.Threshold = threshold;
        }

        public int Do() => this.Threshold.Enalbed ? 1 : 0;
    }
}
