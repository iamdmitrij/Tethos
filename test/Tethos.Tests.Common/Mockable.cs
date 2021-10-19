using System;

namespace Tethos.Tests.Common
{
    public class Mockable : IMockable
    {
        public int Do() => throw new NotImplementedException();
    }
}
