namespace Tethos.NUnit.Demo;

using System;
using global::FakeItEasy;
using global::NUnit.Framework;
using Tethos.FakeItEasy;
using Tethos.Tests.Common;

public class ContainerAsProperty : IDisposable
{
    public ContainerAsProperty()
    {
        this.Container = AutoMocking.Create();
    }

    public IAutoMockingContainer Container { get; }

    [Test]
    [Property("Type", "Demo")]
    public void Exercise_WithMock_ShouldReturn42()
    {
        // Arrange
        var expected = 42;
        var sut = this.Container.Resolve<SystemUnderTest>();
        var mock = this.Container.Resolve<IMockable>();

        A.CallTo(() => mock.Get()).Returns(expected);

        // Act
        var actual = sut.Exercise();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    public void Dispose()
    {
        this.Container?.Dispose();
    }
}
