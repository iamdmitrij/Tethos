using Moq;
using Tethos.Moq;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.xUnit.Demo
{
    public class ContainerFromBaseClass : AutoMockingTest
    {
        [Fact]
        [Trait("", "Demo")]
        public void Do_WithMock_ShouldReturn42()
        {
            // Arrange
            var expected = 42;
            var sut = Container.Resolve<SystemUnderTest>();

            Container.Resolve<Mock<IMockable>>()
                .Setup(x => x.Do())
                .Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            Assert.Equal(actual, expected);
        }
    }
}
