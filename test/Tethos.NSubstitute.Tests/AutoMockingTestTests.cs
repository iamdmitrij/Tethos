namespace Tethos.NSubstitute.Tests
{
    using System;
    using AutoFixture.Xunit2;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Registration;
    using FluentAssertions;
    using global::NSubstitute;
    using Tethos.Extensions;
    using Tethos.Tests.Common;
    using Xunit;

    public class AutoMockingTestTests : AutoMockingTest
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void Container_ShouldHaveAutoResolverInstalled()
        {
            // Assert
            this.AutoResolver.Should().BeOfType<AutoResolver>();
        }

        [Theory]
        [AutoData]
        [Trait("Category", "Integration")]
        public void Test_SimpleDependency_ShouldMatchValue(int expected)
        {
            // Arrange
            var sut = this.Container.Resolve<SystemUnderTest>();
            this.Container.Resolve<IMockable>()
                .Do()
                .Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            actual.Should().Be(expected);
        }

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
            actual.Do();

            // Assert
            mock.Should().BeOfType(expectedType);
            mock.Received().Do();
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
            actual.Do();

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
            actual.Do();

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
            sut.Do();

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
            sut.Do();

            // Assert
            this.Container.Resolve<Concrete>().Should().BeOfType(Substitute.For<Concrete>(100, 200).GetType());
            this.Container.Resolve<Threshold>().Should().BeOfType(Substitute.For<Threshold>(true).GetType());
            this.Container.Resolve<PartialThreshold>().Should().BeOfType(Substitute.For<PartialThreshold>(true).GetType());
            this.Container.Resolve<AbstractThreshold>().Should().BeOfType(Substitute.For<AbstractThreshold>(true).GetType());
        }

        [Theory]
        [AutoData]
        [Trait("Category", "Integration")]
        public void Clean_ShouldRevertBackToOriginalBehavior(Mockable mockable)
        {
            // Arrange
            var sut = this.Container.Resolve<SystemUnderTest>();

            this.Container.Register(Component.For<SystemUnderTest>()
                .OverridesExistingRegistration()
                .DependsOn(Dependency.OnValue<IMockable>(mockable)));

            // Act
            this.Clean();
            var concrete = this.Container.Resolve<SystemUnderTest>();
            Action action = () => concrete.Do();
            sut.Do();

            // Assert
            action.Should().Throw<NotImplementedException>();
        }
    }
}
