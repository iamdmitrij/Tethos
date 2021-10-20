using NSubstitute;
using System;
using Tethos.NSubstitute;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.xUnit.Demo
{
    public class ContainerInjected : IDisposable
    {
        public IAutoNSubstituteContainer Container { get; }

        public ContainerInjected(IAutoNSubstituteContainer container)
        {
            Container = container;
        }

        [Fact]
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
