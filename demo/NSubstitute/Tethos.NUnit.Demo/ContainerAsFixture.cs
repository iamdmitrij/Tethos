using NSubstitute;
using NUnit.Framework;
using Tethos.NSubstitute;
using Tethos.Tests.Common;

namespace Tethos.NUnit.Demo
{
    public class ContainerAsProperty
    {
        public IAutoNSubstituteContainer Container { get; }

        public ContainerAsProperty()
        {
            Container = AutoNSubstituteContainerFactory.Create();
        }

        [Test]
        public void Do_WithMock_ShouldReturn42()
        {
            // Arrange
            var expected = 42;
            var sut = Container.Resolve<SystemUnderTest>();
            var mock = Container.Resolve<IMockable>();

            mock.Do().Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            Assert.AreEqual(actual, expected);
        }
    }
}
