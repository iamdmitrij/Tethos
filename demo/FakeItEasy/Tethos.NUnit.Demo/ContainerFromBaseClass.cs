using FakeItEasy;
using NUnit.Framework;
using Tethos.FakeItEasy;
using Tethos.Tests.Common;

namespace Tethos.NUnit.Demo
{
    public class ContainerFromBaseClass : AutoMockingTest
    {
        [Test]
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
            Assert.AreEqual(actual, expected);
        }
    }
}
