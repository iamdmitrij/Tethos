namespace Tethos.NSubstitute.Tests;

using AutoFixture.Xunit2;
using FluentAssertions;
using global::NSubstitute;
using Tethos.NSubstitute.Tests.Attributes;
using Tethos.Tests.Common;
using Xunit;

public class AutoMockingTests
{
    [Theory]
    [AutoData]
    [Trait("Type", "Integration")]
    public void SystemUnderTest_Exercise_ShouldMatch(int expected)
    {
        // Arrange
        var sut = AutoMocking.Container.Resolve<SystemUnderTest>();
        AutoMocking.Container.Resolve<IMockable>()
            .Get()
            .Returns(expected);

        // Act
        var actual = sut.Exercise();

        // Assert
        actual.Should().Be(expected);
        AutoMocking.Container.Resolve<IMockable>().Received().Get();
    }

    [Theory]
    [AutoMockingContainerData]
    [Trait("Type", "Integration")]
    public void Exercise_WithStaticFactory_ShouldMatch(
        IAutoMockingContainer container,
        int expected)
    {
        // Arrange
        var sut = container.Resolve<SystemUnderTest>();
        container.Resolve<IMockable>()
            .Get()
            .Returns(expected);

        // Act
        var actual = sut.Exercise();

        // Assert
        actual.Should().Be(expected);
    }
}
