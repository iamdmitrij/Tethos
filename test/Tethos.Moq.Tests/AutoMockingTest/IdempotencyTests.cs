namespace Tethos.Moq.Tests.AutoMockingTest
{
    using FluentAssertions;
    using global::Moq;
    using Tethos.Extensions;
    using Tethos.Tests.Common;
    using Xunit;

    public class IdempotencyTests : Moq.AutoMockingTest
    {
        [Fact]
        [Trait("Type", "Integration")]
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
        [Trait("Type", "Integration")]
        public void A_Idempotency_ShouldMatchMocks()
        {
            // Arrange
            var expected = AutoMocking.Container.ResolveFrom<SystemUnderTest, Mock<IMockable>>();

            // Act
            var actual = AutoMocking.Container.ResolveFrom<SystemUnderTest, Mock<IMockable>>();

            // Assert
            actual.Should().BeSameAs(expected);
        }

        [Fact]
        [Trait("Type", "Integration")]
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
        [Trait("Type", "Integration")]
        public void Test_Idempotency_ShouldNotMatch()
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
        public void Test_Idempotency_ShouldMatchMockTypes()
        {
            // Arrange
            _ = this.Container.Resolve<SystemUnderTest>();
            var expected = this.Container.Resolve<Mock<IMockable>>();

            // Act
            var actual = this.Container.Resolve<Mock<IMockable>>();

            // Assert
            actual.Should().BeSameAs(expected);
        }

        [Fact]
        [Trait("Type", "Integration")]
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
        [Trait("Type", "Integration")]
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
        [Trait("Type", "Integration")]
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
        [Trait("Type", "Integration")]
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
        [Trait("Type", "Integration")]
        public void Idempotency_WithProxyObject_ShouldBeSameMockObjects()
        {
            // Arrange
            _ = this.Container.Resolve<SystemUnderTest>();
            var expected = this.Container.Resolve<IMockable>();

            // Act
            var actual = this.Container.Resolve<IMockable>();

            // Assert
            actual.Should().BeSameAs(expected);
        }

        [Fact]
        [Trait("Type", "Integration")]
        public void Idempotency_WithProxyObject_ShouldBeSameMocks()
        {
            // Arrange
            _ = this.Container.Resolve<SystemUnderTest>();
            var expected = Mock.Get(this.Container.Resolve<IMockable>());

            // Act
            var actual = Mock.Get(this.Container.Resolve<IMockable>());

            // Assert
            actual.Should().BeSameAs(expected);
        }
    }
}
