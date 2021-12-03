namespace Tethos.NSubstitute.Tests.AutoMockingTest
{
    using AutoFixture.Xunit2;
    using Castle.MicroKernel;
    using FluentAssertions;
    using global::NSubstitute;
    using Tethos.Extensions;
    using Tethos.Tests.Common;
    using Xunit;

    public class DependencyTests : NSubstitute.AutoMockingTest
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void Container_Resolve_WithClassAndArguments_ShouldMockClass()
        {
            // Arrange
            var expectedType = Substitute.For<Concrete>(100, 200).GetType();
            var actual = this.Container.Resolve<SystemUnderTestClass>(
                new Arguments()
                    .AddNamed("minValue", 100)
                    .AddNamed("maxValue", 200));
            var mock = this.Container.Resolve<Concrete>();

            // Act
            actual.Exercise();

            // Assert
            mock.Should().BeOfType(expectedType);
            mock.Received().Get();
        }

        [Theory]
        [AutoData]
        [Trait("Category", "Integration")]
        public void Container_Resolve_WithClassAndPrimitiveType_ShouldMatchMockTypes(bool value)
        {
            // Arrange
            var expectedType = Substitute.For<Concrete>(100, 200).GetType();
            var expectedThresholdType = Substitute.For<Threshold>(value).GetType();

            var actual = this.Container.Resolve<SystemUnderTwoClasses>(
                new Arguments()
                    .AddDependencyTo<Concrete, int>("minValue", 100)
                    .AddDependencyTo<Concrete, int>("maxValue", 200)
                    .AddDependencyTo<Threshold, bool>("enabled", value));
            var mock = this.Container.Resolve<Concrete>();
            var thresholdMock = this.Container.Resolve<Threshold>();

            // Act
            actual.Exercise();

            // Assert
            mock.Should().BeOfType(expectedType);
            thresholdMock.Should().BeOfType(expectedThresholdType);
        }

        [Theory]
        [AutoData]
        [Trait("Category", "Integration")]
        public void Container_Resolve_WithAbstractClass_ShouldMatchMockTypes(bool value)
        {
            // Arrange
            var expected = Substitute.For<AbstractThreshold>(value).GetType();
            var actual = this.Container.Resolve<SystemUnderAbstractClasses>(
                new Arguments()
                    .AddDependencyTo<AbstractThreshold, bool>("enabled", value));

            // Act
            actual.Exercise();

            // Assert
            this.Container.Resolve<AbstractThreshold>().Should().BeOfType(expected);
        }

        [Theory]
        [AutoData]
        [Trait("Category", "Integration")]
        public void Container_Resolve_WithPartialClass_ShouldMatchMockTypes(bool value)
        {
            // Arrange
            var expected = Substitute.For<PartialThreshold>(value).GetType();
            var sut = this.Container.Resolve<SystemUnderPartialClass>(
                new Arguments()
                    .AddDependencyTo<PartialThreshold, bool>("enabled", value));

            // Act
            sut.Exercise();

            // Assert
            this.Container.Resolve<PartialThreshold>().Should().BeOfType(expected);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Container_Resolve_WithMixedClasses_ShouldCallMock()
        {
            // Arrange
            var sut = this.Container.Resolve<SystemUnderMixedClasses>(
                new Arguments()
                    .AddNamed("demo", 1)
                    .AddTyped(new SealedConcrete())
                    .AddDependencyTo<Concrete, int>("minValue", 100)
                    .AddDependencyTo<Concrete, int>("maxValue", 200)
                    .AddDependencyTo<Threshold, bool>("enabled", true)
                    .AddDependencyTo<PartialThreshold, bool>("enabled", false)
                    .AddDependencyTo<AbstractThreshold, bool>("enabled", false));

            // Act
            sut.Exercise();

            // Assert
            this.Container.Resolve<Concrete>().Should().BeOfType(Substitute.For<Concrete>(100, 200).GetType());
            this.Container.Resolve<Threshold>().Should().BeOfType(Substitute.For<Threshold>(true).GetType());
            this.Container.Resolve<PartialThreshold>().Should().BeOfType(Substitute.For<PartialThreshold>(true).GetType());
            this.Container.Resolve<AbstractThreshold>().Should().BeOfType(Substitute.For<AbstractThreshold>(true).GetType());
        }
    }
}
