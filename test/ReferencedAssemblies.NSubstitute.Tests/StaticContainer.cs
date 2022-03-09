namespace ReferencedAssemblies.NSubstitute.Tests
{
    using global::NSubstitute;
    using ReferencedAssemblies.Common;
    using Tethos.NSubstitute;
    using Xunit;

    public class StaticContainer
    {
        [Fact]
        [Trait("Type", "E2E")]
        public void Exercise_WithMock_ShouldReturn42()
        {
            // Arrange
            var expected = 42;
            var sut = AutoMocking.Container.Resolve<SystemUnderTest>();
            var mock = AutoMocking.Container.Resolve<IMockable>();

            mock.Get().Returns(expected);

            // Act
            var actual = sut.Exercise();

            // Assert
            Assert.Equal(actual, expected);
        }
    }
}