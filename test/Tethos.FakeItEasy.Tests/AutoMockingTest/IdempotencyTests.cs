namespace Tethos.FakeItEasy.Tests.AutoMockingTest
{
    using FluentAssertions;
    using Tethos.Extensions;
    using Tethos.Tests.Common;
    using Xunit;

    public class IdempotencyTests : FakeItEasy.AutoMockingTest
    {
        [Fact]
        [Trait("Type", "Integration")]
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
        [Trait("Type", "Integration")]
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
        [Trait("Type", "Integration")]
        public void Test_Idempotency_ShouldNotMatchObjects()
        {
            // Arrange
            var expected = this.Container.Resolve<SystemUnderTest>();
            var expectedMock = this.Container.Resolve<IMockable>();

            // Act
            var actual = this.Container.Resolve<SystemUnderTest>();
            var actualMock = this.Container.Resolve<IMockable>();

            // Assert
            actual.Should().NotBeSameAs(expected);
            expectedMock.Should().BeSameAs(actualMock);
        }

        [Fact]
        [Trait("Type", "Integration")]
        public void Test_Idempotency_ShouldMatchMocks()
        {
            // Arrange
            _ = this.Container.Resolve<SystemUnderTest>();
            var expected = this.Container.Resolve<IMockable>();

            // Act
            var actual = this.Container.Resolve<IMockable>();

            // Assert
            actual.Should().BeSameAs(expected);
        }
    }
}
