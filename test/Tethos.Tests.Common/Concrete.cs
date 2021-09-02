using System;

namespace Tethos.Tests.Common
{
    public class Concrete : IMockable
    {
        public int Do()
        {
            return new Random().Next(0, 10);
        }
    }
}
