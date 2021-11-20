namespace Tethos.xUnit.Demo
{
    using global::NSubstitute;
    using System;
    using Tethos.NSubstitute;
    using Tethos.Tests.Common;
    using Xunit;

    public class ContainerInjected : IDisposable
    {
        public ContainerInjected(IAutoNSubstituteContainer container)
        {
            this.Container = container;
        }

        public IAutoNSubstituteContainer Container { get; }

        [Fact]
        [Trait("", "Demo")]
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
