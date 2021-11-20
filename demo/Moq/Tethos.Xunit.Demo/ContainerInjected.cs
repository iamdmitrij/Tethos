namespace Tethos.Xunit.Demo
{
    using System;
    using global::Moq;
    using global::Xunit;
    using Tethos.Moq;
    using Tethos.Tests.Common;

    public class ContainerInjected : IDisposable
    {
        public ContainerInjected(IAutoMoqContainer container)
        {
            this.Container = container;
        }

        public IAutoMoqContainer Container { get; }

        [Fact]
        [Trait("", "Demo")]
        public void Do_WithMock_ShouldReturn42()
        {
            // Arrange
            var expected = 42;
            var sut = this.Container.Resolve<SystemUnderTest>();

            this.Container.Resolve<Mock<IMockable>>()
                .Setup(x => x.Do())
                .Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            Assert.Equal(actual, expected);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.Container?.Dispose();
        }
    }
}
