namespace Tethos.NUnit.Demo
{
    using NSubstitute;
    using NUnit.Framework;
    using Tethos.NSubstitute;
    using Tethos.Tests.Common;

    public class ContainerAsProperty
    {
        public IAutoNSubstituteContainer Container { get; }

        public ContainerAsProperty()
        {
            this.Container = AutoNSubstituteContainerFactory.Create();
        }

        [Test]
        public void Do_WithMock_ShouldReturn42()
        {
            // Arrange
            var expected = 42;
            var sut = this.Container.Resolve<SystemUnderTest>();
            var mock = this.Container.Resolve<IMockable>();

            mock.Do().Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            Assert.AreEqual(actual, expected);
        }
    }
}
