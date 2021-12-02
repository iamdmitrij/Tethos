namespace Tethos.NUnit.EndToEnd
{
    using global::FakeItEasy;
    using global::NUnit.Framework;
    using Tethos.FakeItEasy;
    using Tethos.Tests.Common;

    public class ContainerFromBaseClass : AutoMockingTest
    {
        [Test]
        [Category("E2E")]
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
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
