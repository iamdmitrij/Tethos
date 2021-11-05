using System;

namespace Tethos.Tests.Common
{
    public class SystemUnderPartialClass
    {
        public PartialThreshold Threshold { get; }

        public SystemUnderPartialClass(PartialThreshold threshold)
        {
            Threshold = threshold;
        }

        public int Do() => Threshold.Enalbed ? throw new NotImplementedException() : 0;
    }
}
