using FakeItEasy;
using System;
using Tethos.FakeItEasy;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.xUnit.Demo
{
    public class ContainerInjected : IDisposable
    {
        public IAutoFakeItEasyContainer Container { get; }

        public ContainerInjected(IAutoFakeItEasyContainer container)
        {
            Container = container;
        }

        [Fact]
        [Trait("", "Demo")]
        public void Do_WithMock_ShouldReturn42()
        {
            // Arrange
            var expected = 42;
            var sut = Container.Resolve<SystemUnderTest>();
            var mock = Container.Resolve<IMockable>();

            A.CallTo(() => mock.Do()).Returns(expected);

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
