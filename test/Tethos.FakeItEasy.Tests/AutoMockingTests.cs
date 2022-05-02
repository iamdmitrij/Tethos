namespace Tethos.FakeItEasy.Tests;

using AutoFixture.Xunit2;
using FluentAssertions;
using global::FakeItEasy;
using Tethos.FakeItEasy;
using Tethos.FakeItEasy.Tests.Attributes;
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

        A.CallTo(() => AutoMocking.Container.Resolve<IMockable>().Get()).Returns(expected);

        // Act
        var actual = sut.Exercise();

        // Assert
        actual.Should().Be(expected);
        A.CallTo(() => AutoMocking.Container.Resolve<IMockable>().Get()).MustHaveHappened();
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
        var mock = container.Resolve<IMockable>();
        A.CallTo(() => mock.Get()).Returns(expected);

        // Act
        var actual = sut.Exercise();

        // Assert
        actual.Should().Be(expected);
    }
}
