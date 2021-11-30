namespace Tethos.Tests.Common
{
    public class SystemUnderMixedClasses
    {
        public SystemUnderMixedClasses(int demo, Threshold threshold, Concrete mockable, SealedConcrete sealedMockable, PartialThreshold partialThreshold, AbstractThreshold abstractThreshold)
        {
            this.Demo = demo;
            this.SealedMockable = sealedMockable;
            this.Mockable = mockable;
            this.PartialThreshold = partialThreshold;
            this.AbstractThreshold = abstractThreshold;
            this.Threshold = threshold;
        }

        public SealedConcrete SealedMockable { get; }

        public Concrete Mockable { get; }

        public PartialThreshold PartialThreshold { get; }

        public AbstractThreshold AbstractThreshold { get; }

        public Threshold Threshold { get; }

        public int Demo { get; set; }

        public int Exercise() => this.Threshold.Enalbed ? this.Mockable.Get() : 0;
    }
}
