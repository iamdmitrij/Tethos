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
            actual.Should().BeSameAs(expected);
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
            var expected = this.Container.Resolve<Mock<IMockable>>();

            // Act
            var actual = this.Container.Resolve<Mock<IMockable>>();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Idempotency_ResolveFrom_ShouldBeSameMockObjects()
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
        public void Idempotency_ResolveFrom_ShouldBeSameMocks()
        {
            // Arrange
            var expected = Mock.Get(this.Container.ResolveFrom<SystemUnderTest, IMockable>());

            // Act
            var actual = Mock.Get(this.Container.ResolveFrom<SystemUnderTest, IMockable>());

            // Assert
            actual.Should().BeSameAs(expected);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Idempotency_ResolveFromVsResolve_ShouldNotBeSameMockObjects()
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
        public void Idempotency_ResolveFromVsResolve_ShouldBeSameMocks()
        {
            // Arrange
            _ = this.Container.Resolve<SystemUnderTest>();
            var expected = Mock.Get(this.Container.Resolve<IMockable>());

            // Act
            var actual = Mock.Get(this.Container.ResolveFrom<SystemUnderTest, IMockable>());

            // Assert
            actual.Should().BeSameAs(expected);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Idempotency_WithProxyObject_ShouldBeSameMockObjects()
        {
            // Arrange
            _ = this.Container.Resolve<SystemUnderTest>();
            var expected = this.Container.Resolve<IMockable>();

            // Act
            var actual = this.Container.Resolve<IMockable>();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Idempotency_WithProxyObject_ShouldBeSameMocks()
        {
            // Arrange
            _ = this.Container.Resolve<SystemUnderTest>();
            var expected = Mock.Get(this.Container.Resolve<IMockable>());

            // Act
            var actual = Mock.Get(this.Container.Resolve<IMockable>());

            // Assert
            actual.Should().Be(expected);
        }
    }
}
