using System;

namespace Tethos.Tests.Common
{
    public class SystemUnderMixedClasses
    {
        public AbstractThreshold Threshold { get; }

        public SystemUnderMixedClasses(AbstractThreshold threshold)
        {
            Threshold = threshold;
        }

        public int Do() => Threshold.Enalbed ? throw new NotImplementedException() : 0;
    }
}
