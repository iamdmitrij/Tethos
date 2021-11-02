namespace Tethos.xUnit.Demo
{
    using Moq;
    using Tethos.Moq;
    using Tethos.Tests.Common;
    using Xunit;

    public class ContainerFromBaseClass : AutoMockingTest
    {
        [Fact]
        public void Do_WithMock_ShouldReturn42()
        {
            // Arrange
            var expected = 42;
            var sut = this.Container.Resolve<SystemUnderTest>();

            this.Container.Resolve<Mock<IMockable>>()
                .Setup(x => x.Do())
                .Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            Assert.Equal(actual, expected);
        }
    }
}
