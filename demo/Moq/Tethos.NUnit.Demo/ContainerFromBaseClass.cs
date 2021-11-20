using Moq;
using NUnit.Framework;
using Tethos.Moq;
using Tethos.Tests.Common;

namespace Tethos.NUnit.Demo
{
    public class ContainerFromBaseClass : AutoMockingTest
    {
        [Test]
        [Category("Demo")]
        public void Do_WithMock_ShouldReturn42()
        {
            // Arrange
            var expected = 42;
            var sut = this.Container.Resolve<SystemUnderTest>();

            this.Container.Resolve<Mock<IMockable>>()
                .Setup(x => x.Do())
                .Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            Assert.AreEqual(actual, expected);
        }
    }
}
