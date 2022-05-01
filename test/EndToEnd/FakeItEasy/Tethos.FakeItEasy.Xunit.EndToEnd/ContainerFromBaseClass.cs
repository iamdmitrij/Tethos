namespace Tethos.FakeItEasy.Xunit.EndToEnd;

using global::FakeItEasy;
using global::Xunit;
using Tethos.FakeItEasy;
using Tethos.Tests.Common;

public class ContainerFromBaseClass : AutoMockingTest
{
    [Fact]
    [Trait("Type", "E2E")]
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
        Assert.Equal(actual, expected);
    }
}
