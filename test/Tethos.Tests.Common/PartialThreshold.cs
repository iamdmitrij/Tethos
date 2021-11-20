namespace Tethos.Tests.Common
{
    using System;

    public partial class PartialThreshold : AbstractThreshold
    {
        public PartialThreshold(bool enabled)
            : base(enabled)
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public DateTime CreatedOn { get; }
    }

    public partial class PartialThreshold : AbstractThreshold, IDisposable
    {
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) => throw new NotImplementedException();
    }
}
