using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tethos.Moq;
using Tethos.Tests.Common;

namespace Tethos.MSTest.Demo
{
    [TestClass]
    public class ContainerFromBaseClass: AutoMockingTest
    {
        [TestMethod]
        public void Do_WithMock_ShouldReturn42()
        {
            // Arrange
            var expected = 42;
            var sut = Container.Resolve<SystemUnderTest>();

            Container.Resolve<Mock<IMockable>>()
                .Setup(x => x.Do())
                .Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            Assert.AreEqual(actual, expected);
        }
    }
}
