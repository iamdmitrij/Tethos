namespace Tethos.Tests.Common
{
    public abstract class AbstractThreshold
    {
        public bool Enalbed { get; }

        public AbstractThreshold(bool enabled)
        {
            Enalbed = enabled;
        }
    }
}
