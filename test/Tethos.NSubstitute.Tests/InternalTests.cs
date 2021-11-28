namespace Tethos.NSubstitute.Tests
{
    using AutoFixture.Xunit2;
    using Castle.DynamicProxy.Generators;
    using Castle.MicroKernel;
    using FluentAssertions;
    using global::NSubstitute;
    using Tethos.Extensions;
    using Tethos.Tests.Common;
    using Xunit;

    public class InternalTests : AutoMockingTest
    {

        [Theory]
        [AutoData]
        [Trait("Category", "Integration")]
        public void Exercise_InternalClass_ShouldMatch(int expected)
        {
            // Arrange
            var sut = this.Container.Resolve<InternalSystemUnderTest>();
            this.Container.Resolve<IMockable>()
                .Do()
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
            this.Container.Resolve<IInternalMockable>()
                .Do()
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
            var sut = () => this.Container.ResolveFrom<InternalDependency.Tests.SystemUnderTest, InternalDependency.Tests.IMockable>();

            // Act & Assert
            sut.Should().Throw<GeneratorException>();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Exercise_LooseInternalDependency_ShouldThrowComponentNotFoundException()
        {
            // Arrange
            var sut = () => this.Container.Resolve<InternalDependency.Tests.IMockable>();

            // Act & Assert
            sut.Should().Throw<ComponentNotFoundException>();
        }
    }
}
