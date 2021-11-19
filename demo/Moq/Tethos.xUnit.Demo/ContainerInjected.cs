using Moq;
using System;
using Tethos.Moq;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.xUnit.Demo
{
    public class ContainerInjected : IDisposable
    {
        public IAutoMoqContainer Container { get; }

        public ContainerInjected(IAutoMoqContainer container)
        {
            Container = container;
        }

        [Fact]
        [Trait("Category", "Demo")]
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
            Assert.Equal(actual, expected);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Container?.Dispose();
        }
    }
}
