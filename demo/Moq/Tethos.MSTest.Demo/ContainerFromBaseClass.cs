namespace Tethos.MSTest.Demo
{
    using global::Moq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tethos.Moq;
    using Tethos.Tests.Common;

    [TestClass]
    public class ContainerFromBaseClass : AutoMockingTest
    {
        [TestMethod]
        [TestCategory("Demo")]
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
