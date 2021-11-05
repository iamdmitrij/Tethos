namespace Tethos.Tests.Common
{
    public class SystemUnderAbstractClasses
    {
        public AbstractThreshold Threshold { get; }

        public SystemUnderAbstractClasses(AbstractThreshold threshold)
        {
            Threshold = threshold;
        }

        public int Do() => Threshold.Enalbed ? 1 : 0;
    }
}
