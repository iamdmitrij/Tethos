namespace Tethos.Xunit.EndToEnd
{
    using System;
    using global::Moq;
    using global::Xunit;
    using Tethos.Moq;
    using Tethos.Tests.Common;

    public class ContainerInjected : IDisposable
    {
        public ContainerInjected(IAutoMockingContainer container)
        {
            this.Container = container;
        }

        public IAutoMockingContainer Container { get; }

        [Fact]
        [Trait("", "Demo")]
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
