using AutoFixture.Xunit2;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using FluentAssertions;
using NSubstitute;
using System;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.NSubstitute.Tests
{
    public class AutoMockingTestTests : AutoMockingTest
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void Container_ShouldHaveAutoResolverInstalled()
        {
            // Assert
            AutoResolver.Should().BeOfType<AutoNSubstituteResolver>();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Test_Idempotency_ShouldMatchMocks()
        {
            // Arrange
            Container.Resolve<SystemUnderTest>(); // TODO: What if this can be omitted?
            var expected = Container.Resolve<IMockable>();

            // Act
            var actual = Container.Resolve<IMockable>();

            // Assert
            actual.Should().Be(expected);
        }

        [Theory, AutoData]
        [Trait("Category", "Integration")]
        public void Test_SimpleDependency_ShouldMatchValue(int expected)
        {
            // Arrange
            var sut = Container.Resolve<SystemUnderTest>();
            Container.Resolve<IMockable>()
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
            var actual = Container.Resolve<SystemUnderTestClass>(
                new Arguments()
                    .AddNamed("minValue", 100)
                    .AddNamed("maxValue", 200));
            var mock = Container.Resolve<Concrete>();

            // Act
            actual.Do();

            // Assert
            mock.Should().BeOfType(expectedType);
            mock.Received().Do();
        }

        [Theory, AutoData]
        [Trait("Category", "Integration")]
        public void Container_Resolve_WithClassAndPrimitiveType_ShouldMatchMockTypes(bool value)
        {
            // Arrange
            var expectedType = Substitute.For<Concrete>(100, 200).GetType();
            var expectedThresholdType = Substitute.For<Threshold>(value).GetType();

            var actual = Container.Resolve<SystemUnderTwoClasses>(
                new Arguments()
                    .AddDependencyTo<Concrete, int>("minValue", 100)
                    .AddDependencyTo<Concrete, int>("maxValue", 200)
                    .AddDependencyTo<Threshold, bool>("enabled", value));
            var mock = Container.Resolve<Concrete>();
            var thresholdMock = Container.Resolve<Threshold>();

            // Act
            actual.Do();

            // Assert
            mock.Should().BeOfType(expectedType);
            thresholdMock.Should().BeOfType(expectedThresholdType);
        }

        [Theory, AutoData]
        [Trait("Category", "Integration")]
        public void Container_Resolve_WithAbstractClass_ShouldMatchMockTypes(bool value)
        {
            // Arrange
            var expected = Substitute.For<AbstractThreshold>(value).GetType();
            var actual = Container.Resolve<SystemUnderAbstractClasses>(
                new Arguments()
                    .AddDependencyTo<AbstractThreshold, bool>("enabled", value));

            // Act
            actual.Do();

            // Assert
            Container.Resolve<AbstractThreshold>().Should().BeOfType(expected);
        }

        [Theory, AutoData]
        [Trait("Category", "Integration")]
        public void Container_Resolve_WithPartialClass_ShouldMatchMockTypes(bool value)
        {
            // Arrange
            var expected = Substitute.For<PartialThreshold>(value).GetType();
            var sut = Container.Resolve<SystemUnderPartialClass>(
                new Arguments()
                    .AddDependencyTo<PartialThreshold, bool>("enabled", value));

            // Act
            sut.Do();

            // Assert
            Container.Resolve<PartialThreshold>().Should().BeOfType(expected);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Container_Resolve_WithMixedClasses_ShouldCallMock()
        {
            // Arrange
            var sut = Container.Resolve<SystemUnderMixedClasses>(
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
            Container.Resolve<Concrete>().Should().BeOfType(Substitute.For<Concrete>(100, 200).GetType());
            Container.Resolve<Threshold>().Should().BeOfType(Substitute.For<Threshold>(true).GetType());
            Container.Resolve<PartialThreshold>().Should().BeOfType(Substitute.For<PartialThreshold>(true).GetType());
            Container.Resolve<AbstractThreshold>().Should().BeOfType(Substitute.For<AbstractThreshold>(true).GetType());
        }

        [Theory, AutoData]
        [Trait("Category", "Integration")]
        public void Clean_ShouldRevertBackToOriginalBehavior(Mockable mockable)
        {
            // Arrange
            var sut = Container.Resolve<SystemUnderTest>();

            Container.Register(Component.For<SystemUnderTest>()
                .OverridesExistingRegistration()
                .DependsOn(Dependency.OnValue<IMockable>(mockable)));

            // Act
            Clean();
            var concrete = Container.Resolve<SystemUnderTest>();
            Action action = () => concrete.Do();
            sut.Do();

            // Assert
            action.Should().Throw<NotImplementedException>();
        }
    }
}
