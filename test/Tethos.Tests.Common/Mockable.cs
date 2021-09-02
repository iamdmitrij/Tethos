using System;

namespace Tethos.Tests.Common
{
    /// <inheritdoc />
    public class Mockable : IMockable
    {
        /// <inheritdoc />
        public int Do()
        {
            throw new NotImplementedException();
        }
    }
}
