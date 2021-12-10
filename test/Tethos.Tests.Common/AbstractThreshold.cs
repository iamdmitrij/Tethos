namespace Tethos.Tests.Common
{
    public abstract class AbstractThreshold
    {
        protected AbstractThreshold(bool enabled)
        {
            this.Enabled = enabled;
        }

        public bool Enabled { get; }
    }
}
