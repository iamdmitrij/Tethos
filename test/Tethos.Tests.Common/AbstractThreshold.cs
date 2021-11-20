namespace Tethos.Tests.Common
{
    public abstract class AbstractThreshold
    {
        protected AbstractThreshold(bool enabled)
        {
            this.Enalbed = enabled;
        }

        public bool Enalbed { get; }
    }
}
