namespace Tethos.Tests.Common
{
    public abstract class AbstractThreshold
    {
        public AbstractThreshold(bool enabled)
        {
            this.Enalbed = enabled;
        }

        public bool Enalbed { get; }
    }
}
