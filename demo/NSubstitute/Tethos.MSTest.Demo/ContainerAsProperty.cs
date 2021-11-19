using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Tethos.NSubstitute;
using Tethos.Tests.Common;

namespace Tethos.MSTest.Demo
{
    [TestClass]
    public class ContainerAsProperty
    {
        public IAutoNSubstituteContainer Container { get; }

        public ContainerAsProperty()
        {
            Container = AutoNSubstituteContainerFactory.Create();
        }

        [TestMethod]
        [TestProperty("Category", "Demo")]
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
