namespace Tethos.NSubstitute.Tests.AutoMockingTest
{
    using FluentAssertions;
    using Tethos.Extensions;
    using Tethos.Tests.Common;
    using Xunit;

    public class IdempotencyTests : NSubstitute.AutoMockingTest
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void ResolveFrom_Idempotency_ShouldMatchMocks()
        {
            // Arrange
            var expected = this.Container.ResolveFrom<SystemUnderTest, IMockable>();

            // Act
            var actual = this.Container.ResolveFrom<SystemUnderTest, IMockable>();

            // Assert
            actual.Should().BeSameAs(expected);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Test_Idempotency_ResolveFromVsResolve_ShouldMatchMocks()
        {
            // Arrange
            _ = this.Container.Resolve<SystemUnderTest>();
            var expected = this.Container.Resolve<IMockable>();

            // Act
            var actual = this.Container.ResolveFrom<SystemUnderTest, IMockable>();

            // Assert
            actual.Should().BeSameAs(expected);
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
            var expected = this.Container.Resolve<IMockable>();

            // Act
            var actual = this.Container.Resolve<IMockable>();

            // Assert
            actual.Should().Be(expected);
        }
    }
}
