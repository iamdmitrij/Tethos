namespace Tethos.Tests.Common
{
    using System;

    public class Mockable : IMockable
    {
        public int Get() => throw new NotImplementedException();
    }
}
