namespace Tethos.Xunit.Demo;

using System;
using global::NSubstitute;
using global::Xunit;
using Microsoft.Extensions.DependencyInjection;
using Tethos.NSubstitute;
using Tethos.Tests.Common;

public class ContainerInjected : IDisposable
{
    private readonly ServiceProvider provider;
    private readonly IServiceScope scope;

    public ContainerInjected()
    {
        var services = new ServiceCollection();
        new Startup().ConfigureServices(services);
        this.provider = services.BuildServiceProvider();
        this.scope = this.provider.CreateScope();
        this.Container = this.scope.ServiceProvider.GetRequiredService<IAutoMockingContainer>();
    }

    public IAutoMockingContainer Container { get; }

    [Fact]
    [Trait("Type", "Demo")]
    public void Exercise_WithMock_ShouldReturn42()
    {
        // Arrange
        var expected = 42;
        var sut = this.Container.Resolve<SystemUnderTest>();
        var mock = this.Container.Resolve<IMockable>();

        mock.Get().Returns(expected);

        // Act
        var actual = sut.Exercise();

        // Assert
        Assert.Equal(actual, expected);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        this.scope.Dispose();
        this.provider.Dispose();
    }
}
