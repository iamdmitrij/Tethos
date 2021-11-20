namespace Tethos.Tests.Common
{
    using System;

    public sealed partial class PartialThreshold : AbstractThreshold
    {
        public PartialThreshold(bool enabled)
            : base(enabled)
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public DateTime CreatedOn { get; }
    }

    public sealed partial class PartialThreshold : AbstractThreshold, IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
