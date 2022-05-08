namespace Tethos.Tests.Extensions.Assembly;

using System;
using System.Reflection;

public class AssemblyStub : Assembly
{
    public override string FullName => $"{Guid.NewGuid()}";
}
