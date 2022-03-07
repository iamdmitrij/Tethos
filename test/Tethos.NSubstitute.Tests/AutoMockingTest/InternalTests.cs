namespace Tethos.NSubstitute.Tests.AutoMockingTest
{
    using AutoFixture.Xunit2;
    using Castle.DynamicProxy.Generators;
    using Castle.MicroKernel;
    using FluentAssertions;
    using global::NSubstitute;
    using Tethos.Extensions;
    using Tethos.Tests.Common;
    using Xunit;

    public class InternalTests : NSubstitute.AutoMockingTest
    {
        public override AutoMockingConfiguration AutoMockingConfiguration => new () { IncludeNonPublicTypes = true };

        [Theory]
        [AutoData]
        [Trait("Type", "Integration")]
        public void Exercise_InternalClass_ShouldMatch(int expected)
        {
            // Arrange
            var sut = this.Container.Resolve<InternalSystemUnderTest>();
            this.Container.Resolve<IMockable>()
                .Get()
                .Returns(expected);

            // Act
            var actual = sut.Exercise();

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [AutoData]
        [Trait("Type", "Integration")]
        public void Exercise_InternalDependency_ShouldMatch(int expected)
        {
            // Arrange
            var sut = this.Container.Resolve<SystemUnderTestWithInternal>();
            this.Container.Resolve<IInternalMockable>()
                .Get()
                .Returns(expected);

            // Act
            var actual = sut.Exercise();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        [Trait("Type", "Integration")]
        public void Resolve_WeakNamedAssembly_ShouldThrowGeneratorException()
        {
            // Arrange
            var sut = () => this.Container.Resolve<Tethos.Tests.Common.WeakNamed.SystemUnderTest>();

            // Act & Assert
            sut.Should().Throw<GeneratorException>();
        }

        [Fact]
        [Trait("Type", "Integration")]
        public void ResolveFrom_WeakNamedAssembly_ShouldThrowGeneratorException()
        {
            // Arrange
            var sut = () => this.Container.ResolveFrom<Tethos.Tests.Common.WeakNamed.SystemUnderTest, Tethos.Tests.Common.WeakNamed.IMockable>();

            // Act & Assert
            sut.Should().Throw<GeneratorException>();
        }

        [Fact]
        [Trait("Type", "Integration")]
        public void Resolve_MockFromWeakNamedAssembly_ShouldThrowComponentNotFoundException()
        {
            // Arrange
            var sut = () => this.Container.Resolve<Tethos.Tests.Common.WeakNamed.IMockable>();

            // Act & Assert
            sut.Should().Throw<ComponentNotFoundException>();
        }
    }
}
