namespace Tethos.Tests.Common
{
    public class SystemUnderMixedClasses
    {
        public SealedConcrete SealedMockable { get; }

        public Concrete Mockable { get; }

        public PartialThreshold PartialThreshold { get; }

        public AbstractThreshold AbstractThreshold { get; }

        public Threshold Threshold { get; }

        public int Demo { get; set; }

        public SystemUnderMixedClasses(int demo, Threshold threshold, Concrete mockable, SealedConcrete sealedMockable, PartialThreshold partialThreshold, AbstractThreshold abstractThreshold)
        {
            Demo = demo;
            SealedMockable = sealedMockable;
            Mockable = mockable;
            PartialThreshold = partialThreshold;
            AbstractThreshold = abstractThreshold;
            Threshold = threshold;
        }

        public int Do() => Threshold.Enalbed ? Mockable.Do() : 0;
    }
}
