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
        public void Container_ShouldHaveAutoResolverInstalled()
        {
            // Act
            var actual = Container.Resolve<ISubDependencyResolver>();

            // Assert
            actual.Should().BeOfType<AutoNSubstituteResolver>();
        }

        [Theory, AutoData]
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
        public void Container_Resolve_WithClassAndArguments_ShouldMockClass()
        {
            // Arrange
            var expectedType = Substitute.For<Concrete>(100, 200).GetType();
            var actual = Container.Resolve<SystemUnderTestClass>(
                new Arguments()
                    .AddNamed("minValue", 100)
                    .AddNamed("maxValue", 200)
            );
            var mock = Container.Resolve<Concrete>();

            // Act
            actual.Do();

            // Assert
            mock.Should().BeOfType(expectedType);
            mock.Received().Do();
        }

        [Fact]
        public void Container_Resolve_WithClassAndPrimitiveType_ShouldMockClass()
        {
            // Arrange
            var expectedType = Substitute.For<Concrete>(100, 200).GetType();
            var expectedThresholdType = Substitute.For<Threshold>(true).GetType();

            var actual = Container.Resolve<SystemUnderTwoClasses>(
                new Arguments()
                    .AddDependencyTo<Concrete, int>("minValue", 100)
                    .AddDependencyTo<Concrete, int>("maxValue", 200)
                    .AddDependencyTo<Threshold, bool>("enabled", true)
            );
            var mock = Container.Resolve<Concrete>();
            var thresholdMock = Container.Resolve<Threshold>();

            // Act
            actual.Do();

            // Assert
            mock.Should().BeOfType(expectedType);
            mock.Received().Do();
            thresholdMock.Should().BeOfType(expectedThresholdType);
        }

        [Theory, AutoData]
        public void Clean_ShouldRevertBackToOriginalBehavior(Mockable mockable)
        {
            // Arrange
            var sut = Container.Resolve<SystemUnderTest>();

            Container.Register(Component.For<SystemUnderTest>()
                .OverridesExistingRegistration()
                .DependsOn(Dependency.OnValue<IMockable>(mockable))
            );

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
