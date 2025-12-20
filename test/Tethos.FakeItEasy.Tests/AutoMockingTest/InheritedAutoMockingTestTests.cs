namespace Tethos.FakeItEasy.Tests.AutoMockingTest;

using AutoFixture.Xunit3;
using global::FakeItEasy;
using Tethos.FakeItEasy.Tests.AutoMockingTest.SUT;
using Xunit;

public class InheritedAutoMockingTestTests
{
    [Theory]
    [AutoData]
    [Trait("Type", "Unit")]
    public void Dispose_ShouldDisposeContainer(InheritedAutoMockingTest sut)
    {
        // Act
        sut.Dispose();

        // Assert
        A.CallTo(() => sut.Container.Dispose()).MustHaveHappened();
    }

    [Theory]
    [AutoData]
    [Trait("Type", "Unit")]
    public void Dispose_NullContainer_ShouldNotDisposeContainer(InheritedAutoMockingTest sut)
    {
        // Arrange
        sut.Container = null;

        // Act
        sut.Dispose();

        // Assert
        A.CallTo(() => sut.Proxy.Dispose()).MustNotHaveHappened();
    }
}
