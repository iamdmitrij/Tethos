namespace ReferencedAssemblies.FakeItEasy.Tests
{
    using global::FakeItEasy;
    using ReferencedAssemblies.Common;
    using Tethos.FakeItEasy;
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
            var mock = AutoMocking.Container.Resolve<IMockable>();

            A.CallTo(() => mock.Get()).Returns(expected);

            // Act
            var actual = sut.Exercise();

            // Assert
            Assert.Equal(actual, expected);
        }
    }
}
