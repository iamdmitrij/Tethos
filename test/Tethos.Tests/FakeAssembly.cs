namespace Tethos.Tests
{
    using System;
    using System.Reflection;

    public class FakeAssembly : Assembly
    {
        public override string FullName => $"{Guid.NewGuid()}";
    }
}
