namespace Tethos.MSTest.Demo
{
    using FakeItEasy;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tethos.FakeItEasy;
    using Tethos.Tests.Common;

    [TestClass]
    public class ContainerAsProperty
    {
        public IAutoFakeItEasyContainer Container { get; }

        public ContainerAsProperty()
        {
            this.Container = AutoFakeItEasyContainerFactory.Create();
        }

        [TestMethod]
        public void Do_WithMock_ShouldReturn42()
        {
            // Arrange
            var expected = 42;
            var sut = this.Container.Resolve<SystemUnderTest>();
            var mock = this.Container.Resolve<IMockable>();

            A.CallTo(() => mock.Do()).Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            Assert.AreEqual(actual, expected);
        }
    }
}
