namespace Tethos.Moq.MSTest.EndToEnd
{
    using global::Moq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tethos.Moq;
    using Tethos.Tests.Common;

    [TestClass]
    public class ContainerAsProperty
    {
        public ContainerAsProperty()
        {
            this.Container = AutoMockingContainerFactory.Create();
        }

        public IAutoMockingContainer Container { get; }

        [TestMethod]
        [TestProperty("Type", "E2E")]
        public void Exercise_WithMock_ShouldReturn42()
        {
            // Arrange
            var expected = 42;
            var sut = this.Container.Resolve<SystemUnderTest>();

            this.Container.Resolve<Mock<IMockable>>()
                .Setup(mock => mock.Get())
                .Returns(expected);

            // Act
            var actual = sut.Exercise();

            // Assert
            Assert.AreEqual(actual, expected);
        }
    }
}
