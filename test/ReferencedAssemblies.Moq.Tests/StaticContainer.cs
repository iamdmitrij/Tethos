namespace Tethos.Moq.Referenced.Tests
{
    using global::Moq;
    using ReferencedAssemblies.Common;
    using Tethos.Moq;
    using Xunit;

    public class StaticContainer
    {
        [Fact]
        [Trait("Type", "Integration")]
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
