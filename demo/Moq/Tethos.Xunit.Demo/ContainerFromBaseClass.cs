namespace Tethos.Xunit.Demo
{
    using global::Moq;
    using global::Xunit;
    using Tethos.Moq;
    using Tethos.Tests.Common;

    public class ContainerFromBaseClass : AutoMockingTest
    {
        [Fact]
        [Trait("", "Demo")]
        public void Do_WithMock_ShouldReturn42()
        {
            // Arrange
            var expected = 42;
            var sut = this.Container.Resolve<SystemUnderTest>();

            this.Container.Resolve<Mock<IMockable>>()
                .Setup(mock => mock.Do())
                .Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            Assert.Equal(actual, expected);
        }
    }
}
