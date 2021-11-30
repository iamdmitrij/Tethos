namespace Tethos.Xunit.Demo
{
    using System;
    using global::FakeItEasy;
    using global::Xunit;
    using Tethos.FakeItEasy;
    using Tethos.Tests.Common;

    public class ContainerInjected : IDisposable
    {
        public ContainerInjected(IAutoFakeItEasyContainer container)
        {
            this.Container = container;
        }

        public IAutoFakeItEasyContainer Container { get; }

        [Fact]
        [Trait("", "Demo")]
        public void Exercise_WithMock_ShouldReturn42()
        {
            // Arrange
            var expected = 42;
            var sut = this.Container.Resolve<SystemUnderTest>();
            var mock = this.Container.Resolve<IMockable>();

            A.CallTo(() => mock.Get()).Returns(expected);

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
