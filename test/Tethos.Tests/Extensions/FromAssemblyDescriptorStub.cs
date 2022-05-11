namespace Tethos.Tests.Extensions;

using Castle.MicroKernel.Registration;
using Tethos.Tests.Extensions.Assembly;

public class FromAssemblyDescriptorStub : FromAssemblyDescriptor
{
    public FromAssemblyDescriptorStub()
        : base(new AssemblyStub(), _ => true)
    {
    }

    public bool IncludesNonPublicTypes => this.nonPublicTypes;
}
