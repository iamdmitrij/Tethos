namespace Tethos.Tests.Common
{
    using System;

    public partial class PartialThreshold : AbstractThreshold
    {
        public DateTime CreatedOn { get; }

        public PartialThreshold(bool enabled)
            : base(enabled)
        {
            this.CreatedOn = DateTime.UtcNow;
        }
    }

    public partial class PartialThreshold : AbstractThreshold, IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
