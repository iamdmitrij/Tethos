using FakeItEasy;
using Tethos.FakeItEasy;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.xUnit.Demo
{
    public class ContainerFromBaseClass: AutoMockingTest
    {
        [Fact]
        public void Do_WithMock_ShouldReturn42()
        {
            // Arrange
            var expected = 42;
            var sut = Container.Resolve<SystemUnderTest>();
            var mock = Container.Resolve<IMockable>();

            A.CallTo(() => mock.Do()).Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            Assert.Equal(actual, expected);
        }
    }
}
