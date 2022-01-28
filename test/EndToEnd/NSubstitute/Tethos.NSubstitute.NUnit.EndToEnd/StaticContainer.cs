namespace Tethos.NSubstitute.NUnit.EndToEnd
{
    using global::NSubstitute;
    using global::NUnit.Framework;
    using Tethos.NSubstitute;
    using Tethos.Tests.Common;

    public class StaticContainer
    {
        [Test]
        [Property("Type", "E2E")]
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
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
