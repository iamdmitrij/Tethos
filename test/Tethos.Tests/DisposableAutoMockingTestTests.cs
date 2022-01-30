namespace Tethos.Tests
{
    using FluentAssertions;
    using Xunit;

    public class DisposableAutoMockingTestTests
    {
        [Fact]
        [Trait("Type", "Unit")]
        public void Dispose_ShouldBeDisposing()
        {
            // Arrange
            var sut = new DisposableAutoMockingTest();

            // Act
            sut.Dispose();
            var actual = sut.Disposing;

            // Assert
            actual.Should().BeTrue();
        }
    }
}
