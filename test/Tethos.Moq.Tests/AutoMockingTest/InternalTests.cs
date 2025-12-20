namespace Tethos.Moq.Tests.AutoMockingTest;

using System;
using AutoFixture.Xunit3;
using FluentAssertions;
using global::Moq;
using Tethos.Extensions;
using Tethos.Tests.Common;
using Xunit;

public class InternalTests : Moq.AutoMockingTest
{
    public override AutoMockingConfiguration AutoMockingConfiguration => new() { IncludeNonPublicTypes = true };

    [Theory]
    [AutoData]
    [Trait("Type", "Integration")]
    public void Exercise_InternalClass_ShouldMatch(int expected)
    {
        // Arrange
        var sut = this.Container.Resolve<InternalSystemUnderTest>();

        this.Container.Resolve<Mock<IMockable>>()
            .Setup(mock => mock.Get())
            .Returns(expected);

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

        this.Container.Resolve<Mock<IInternalMockable>>()
            .Setup(mock => mock.Get())
            .Returns(expected);

        // Act
        var actual = sut.Exercise();

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    [Trait("Type", "Integration")]
    public void Resolve_WeakNamedAssembly_ShouldThrowArgumentExceptionException()
    {
        // Arrange
        var sut = () => this.Container.Resolve<Tethos.Tests.Common.WeakNamed.SystemUnderTest>();

        // Act & Assert
        sut.Should().Throw<ArgumentException>();
    }

    [Fact]
    [Trait("Type", "Integration")]
    public void ResolveFrom_WeakNamedAssembly_ShouldThrowArgumentException()
    {
        // Arrange
        var sut = () => this.Container.ResolveFrom<Tethos.Tests.Common.WeakNamed.SystemUnderTest, Mock<Tethos.Tests.Common.WeakNamed.IMockable>>();

        // Act & Assert
        sut.Should().Throw<ArgumentException>();
    }

    [Fact]
    [Trait("Type", "Integration")]
    public void Exercise_LooseInternalDependency_ShouldMatch()
    {
        // Act
        var sut = this.Container.Resolve<Mock<Tethos.Tests.Common.WeakNamed.IMockable>>();

        // Assert
        sut.Should().NotBeNull();
    }
}
