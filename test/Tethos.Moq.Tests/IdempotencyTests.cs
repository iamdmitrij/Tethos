namespace Tethos.Moq.Tests
{
    using FluentAssertions;
    using global::Moq;
    using Tethos.Tests.Common;
    using Xunit;

    public class IdempotencyTests : AutoMockingTest
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void Test_Idempotency_ShouldMatchMocks()
        {
            // Arrange
            var expected = this.Container.Resolve<Mock<IMockable>>();

            // Act
            var actual = this.Container.Resolve<Mock<IMockable>>();

            // Assert
            actual.Should().Be(expected);
        }
    }
}
