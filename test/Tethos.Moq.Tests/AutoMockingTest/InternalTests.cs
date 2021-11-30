namespace Tethos.Moq.Tests.AutoMockingTest
{
    using AutoFixture.Xunit2;
    using Castle.DynamicProxy.Generators;
    using FluentAssertions;
    using global::Moq;
    using Tethos.Extensions;
    using Tethos.Tests.Common;
    using Xunit;

    public class InternalTests : Moq.AutoMockingTest
    {
        [Theory]
        [AutoData]
        [Trait("Category", "Integration")]
        public void Exercise_InternalClass_ShouldMatch(int expected)
        {
            // Arrange
            var sut = this.Container.Resolve<InternalSystemUnderTest>();

            this.Container.Resolve<Mock<IMockable>>()
                .Setup(mock => mock.Do())
                .Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [AutoData]
        [Trait("Category", "Integration")]
        public void Exercise_InternalDependency_ShouldMatch(int expected)
        {
            // Arrange
            var sut = this.Container.Resolve<SystemUnderTestWithInternal>();

            this.Container.Resolve<Mock<IInternalMockable>>()
                .Setup(mock => mock.Do())
                .Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Resolve_LooseInternalDependency_ShouldThrowGeneratorException()
        {
            // Arrange
            var sut = () => this.Container.Resolve<InternalDependency.Tests.SystemUnderTest>();

            // Act & Assert
            sut.Should().Throw<GeneratorException>();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void ResolveFrom_LooseInternalDependency_ShouldThrowGeneratorException()
        {
            // Arrange
            var sut = () => this.Container.ResolveFrom<InternalDependency.Tests.SystemUnderTest, Mock<InternalDependency.Tests.IMockable>>();

            // Act & Assert
            sut.Should().Throw<GeneratorException>();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Exercise_LooseInternalDependency_ShouldMatch()
        {
            // Act
            var sut = this.Container.Resolve<Mock<InternalDependency.Tests.IMockable>>();

            // Assert
            sut.Should().NotBeNull();
        }
    }
}
