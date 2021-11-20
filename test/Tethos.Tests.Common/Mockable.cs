namespace Tethos.Tests.Common
{
    using System;

    public class Mockable : IMockable
    {
        public int Do() => throw new NotImplementedException();
    }
}
