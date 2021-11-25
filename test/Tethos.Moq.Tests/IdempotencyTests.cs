namespace Tethos.Moq.Tests
{
    using FluentAssertions;
    using global::Moq;
    using Tethos.Extensions;
    using Tethos.Tests.Common;
    using Xunit;

    public class IdempotencyTests : AutoMockingTest
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void ResolveFrom_Idempotency_ShouldMatchMocks()
        {
            // Arrange
            var expected = this.Container.ResolveFrom<SystemUnderTest, Mock<IMockable>>();

            // Act
            var actual = this.Container.ResolveFrom<SystemUnderTest, Mock<IMockable>>();

            // Assert
            actual.Should().NotBeSameAs(expected);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Test_Idempotency_ResolveFromVsResolve_ShouldMatchMocks()
        {
            // Arrange
            _ = this.Container.Resolve<SystemUnderTest>();
            var expected = this.Container.Resolve<Mock<IMockable>>();

            // Act
            var actual = this.Container.ResolveFrom<SystemUnderTest, Mock<IMockable>>();

            // Assert
            actual.Should().NotBeSameAs(expected);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Test_Idempotency_ShouldMatchMocks()
        {
            // Arrange
            var expected = this.Container.Resolve<SystemUnderTest>();

            // Act
            var actual = this.Container.Resolve<SystemUnderTest>();

            // Assert
            actual.Should().NotBe(expected); // TODO: Is this ok though?
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Test_Idempotency_ShouldMatchMockTypes()
        {
            // Arrange
            _ = this.Container.Resolve<SystemUnderTest>();
            var expected = this.Container.Resolve<Mock<IMockable>>();

            // Act
            var actual = this.Container.Resolve<Mock<IMockable>>();

            // Assert
            actual.Should().Be(expected);
        }
    }
}
