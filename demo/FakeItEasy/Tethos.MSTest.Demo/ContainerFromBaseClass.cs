using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tethos.FakeItEasy;
using Tethos.Tests.Common;

namespace Tethos.MSTest.Demo
{
    [TestClass]
    public class ContainerFromBaseClass : AutoMockingTest
    {
        [TestMethod]
        [TestCategory("Demo")]
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
