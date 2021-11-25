namespace Tethos.Tests
{
    using System;
    using System.Reflection;

    public class AssemblyStub : Assembly
    {
        public override string FullName => $"{Guid.NewGuid()}";
    }
}
