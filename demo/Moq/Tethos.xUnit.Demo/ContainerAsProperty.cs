namespace Tethos.xUnit.Demo
{
    using System;
    using Moq;
    using Tethos.Moq;
    using Tethos.Tests.Common;
    using Xunit;

    public class ContainerAsProperty : IDisposable
    {
        public IAutoMoqContainer Container { get; }

        public ContainerAsProperty()
        {
            this.Container = AutoMoqContainerFactory.Create();
        }

        [Fact]
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
