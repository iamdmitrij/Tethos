namespace Tethos.MSTest.Demo
{
    using global::NSubstitute;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tethos.NSubstitute;
    using Tethos.Tests.Common;

    [TestClass]
    public class ContainerFromBaseClass : AutoMockingTest
    {
        [TestMethod]
        [TestCategory("Demo")]
        public void Exercise_WithMock_ShouldReturn42()
        {
            // Arrange
            var expected = 42;
            var sut = this.Container.Resolve<SystemUnderTest>();
            var mock = this.Container.Resolve<IMockable>();

            mock.Get().Returns(expected);

            // Act
            var actual = sut.Exercise();

            // Assert
            Assert.AreEqual(actual, expected);
        }
    }
}
