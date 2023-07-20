namespace Tethos.FakeItEasy.Tests.AutoMockingTest;

using AutoFixture.Xunit2;
using Castle.MicroKernel;
using FluentAssertions;
using global::FakeItEasy;
using global::FakeItEasy.Core;
using Tethos.Extensions;
using Tethos.Tests.Common;
using Xunit;

public class InternalTests : FakeItEasy.AutoMockingTest
{
    public override AutoMockingConfiguration AutoMockingConfiguration => new() { IncludeNonPublicTypes = true };

    [Theory]
    [AutoData]
    [Trait("Type", "Integration")]
    public void Exercise_InternalClass_ShouldMatch(int expected)
    {
        // Arrange
        var sut = this.Container.Resolve<InternalSystemUnderTest>();
        var mock = this.Container.Resolve<IMockable>();
        A.CallTo(() => mock.Get()).Returns(expected);

        // Act
        var actual = sut.Exercise();

        // Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [AutoData]
    [Trait("Type", "Integration")]
    public void Exercise_InternalDependency_ShouldMatch(int expected)
    {
        // Arrange
        var sut = this.Container.Resolve<SystemUnderTestWithInternal>();
        var mock = this.Container.Resolve<IInternalMockable>();
        A.CallTo(() => mock.Get()).Returns(expected);

        // Act
        var actual = sut.Exercise();

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    [Trait("Type", "Integration")]
    public void Resolve_WeakNamedAssembly_ShouldThrowFakeCreationException()
    {
        // Arrange
        var sut = () => this.Container.Resolve<Tethos.Tests.Common.WeakNamed.SystemUnderTest>();

        // Act & Assert
        sut.Should().Throw<FakeCreationException>();
    }

    [Fact]
    [Trait("Type", "Integration")]
    public void ResolveFrom_WeakNamedAssembly_ShouldThrowFakeCreationException()
    {
        // Arrange
        var sut = () => this.Container.ResolveFrom<Tethos.Tests.Common.WeakNamed.SystemUnderTest, Tethos.Tests.Common.WeakNamed.IMockable>();

        // Act & Assert
        sut.Should().Throw<FakeCreationException>();
    }

    [Fact]
    [Trait("Type", "Integration")]
    public void Resolve_MockFromWeakNamedAssembly_ShouldThrowComponentNotFoundException()
    {
        // Arrange
        var sut = () => this.Container.Resolve<Tethos.Tests.Common.WeakNamed.IMockable>();

        // Act & Assert
        sut.Should().Throw<ComponentNotFoundException>();
    }
}
