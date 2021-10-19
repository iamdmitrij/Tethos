using System;
using System.Reflection;

namespace Tethos.Tests
{
    public class FakeAssembly : Assembly
    {
        public override string FullName => Guid.NewGuid().ToString();
    }
}
