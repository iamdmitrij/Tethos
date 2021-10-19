using System;

namespace Tethos.Tests.Common
{
    public class Concrete : IMockable
    {
        public int Do() => new Random().Next(0, 10);
    }
}
