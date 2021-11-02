namespace Tethos.NUnit.Demo
{
    using Moq;
    using NUnit.Framework;
    using Tethos.Moq;
    using Tethos.Tests.Common;

    public class ContainerAsProperty
    {
        public IAutoMoqContainer Container { get; }

        public ContainerAsProperty()
        {
            Container = AutoMoqContainerFactory.Create();
        }

        [Test]
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
