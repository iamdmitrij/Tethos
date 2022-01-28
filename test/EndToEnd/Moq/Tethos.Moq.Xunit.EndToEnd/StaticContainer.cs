namespace Tethos.Moq.Xunit.EndToEnd
{
    using global::Moq;
    using global::Xunit;
    using Tethos.Moq;
    using Tethos.Tests.Common;

    public class StaticContainer
    {
        [Fact]
        [Trait("Type", "E2E")]
        public void Exercise_WithMock_ShouldReturn42()
        {
            // Arrange
            var expected = 42;
            var sut = AutoMocking.Container.Resolve<SystemUnderTest>();

            AutoMocking.Container.Resolve<Mock<IMockable>>()
                .Setup(mock => mock.Get())
                .Returns(expected);

            // Act
            var actual = sut.Exercise();

            // Assert
            Assert.Equal(actual, expected);
        }
    }
}
