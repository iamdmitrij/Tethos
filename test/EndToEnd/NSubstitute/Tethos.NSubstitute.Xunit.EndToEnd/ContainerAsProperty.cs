namespace Tethos.NSubstitute.Xunit.EndToEnd
{
    using System;
    using global::NSubstitute;
    using global::Xunit;
    using Tethos.NSubstitute;
    using Tethos.Tests.Common;

    public class ContainerAsProperty : IDisposable
    {
        public ContainerAsProperty()
        {
            this.Container = AutoMocking.Create();
        }

        public IAutoMockingContainer Container { get; }

        [Fact]
        [Trait("Type", "E2E")]
        public void Exercise_WithMock_ShouldReturn42()
        {
            // Arrange
            var expected = 42;
            var sut = this.Container.Resolve<SystemUnderTest>();
            var mock = this.Container.Resolve<IMockable>();

            mock.Get().Returns(expected);

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
