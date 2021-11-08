using AutoFixture.Xunit2;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.FakeItEasy.Tests
{
    public class AutoMockingTestTests : AutoMockingTest
    {
        [Fact]
        public void Container_ShouldHaveAutoResolverInstalled()
        {
            // Act
            var actual = Container.Resolve<ISubDependencyResolver>();

            // Assert
            actual.Should().BeOfType<AutoFakeItEasyResolver>();
        }

        [Theory, AutoData]
        public void Test_SimpleDependency_ShouldMatchValue(int expected)
        {
            // Arrange
            var sut = Container.Resolve<SystemUnderTest>();
            var mock = Container.Resolve<IMockable>();
            A.CallTo(() => mock.Do()).Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Container_Resolve_WithClassAndArguments_ShouldMockClass()
        {
            // Arrange
            var expectedType = A.Fake<Concrete>(options => options.WithArgumentsForConstructor(new List<object>() { 100, 200 })).GetType();
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

            A.CallTo(() => mock.Do()).MustHaveHappened();
        }

        [Theory, AutoData]
        public void Container_Resolve_WithClassAndPrimitiveType_ShouldMatchMockTypes(bool value)
        {
            // Arrange
            var expectedType = A.Fake<Concrete>(options => options.WithArgumentsForConstructor(new List<object>() { 100, 200 })).GetType();
            var expectedThresholdType = A.Fake<Threshold>(options => options.WithArgumentsForConstructor(new List<object>() { value })).GetType();

            var actual = Container.Resolve<SystemUnderTwoClasses>(
                new Arguments()
                    .AddDependencyTo<Concrete, int>("minValue", 100)
                    .AddDependencyTo<Concrete, int>("maxValue", 200)
                    .AddDependencyTo<Threshold, bool>("enabled", value)
            );
            var mock = Container.Resolve<Concrete>();
            var thresholdMock = Container.Resolve<Threshold>();

            // Act
            actual.Do();

            // Assert
            mock.Should().BeOfType(expectedType);
            thresholdMock.Should().BeOfType(expectedThresholdType);
        }

        [Theory, AutoData]
        public void Container_Resolve_WithAbstractClass_ShouldMatchMockTypes(bool value)
        {
            // Arrange
            var expected = A.Fake<AbstractThreshold>(options => options.WithArgumentsForConstructor(new List<object>() { value })).GetType();
            var actual = Container.Resolve<SystemUnderAbstractClasses>(
                new Arguments()
                    .AddDependencyTo<AbstractThreshold, bool>("enabled", value)
            );

            // Act
            actual.Do();

            // Assert
            Container.Resolve<AbstractThreshold>().Should().BeOfType(expected);
        }

        [Theory, AutoData]
        public void Container_Resolve_WithPartialClass_ShouldMatchMockTypes(bool value)
        {
            // Arrange
            var expected = A.Fake<PartialThreshold>(options => options.WithArgumentsForConstructor(new List<object>() { value })).GetType();
            var sut = Container.Resolve<SystemUnderPartialClass>(
                new Arguments()
                    .AddDependencyTo<PartialThreshold, bool>("enabled", value)
            );

            // Act
            sut.Do();

            // Assert
            Container.Resolve<PartialThreshold>().Should().BeOfType(expected);
        }

        [Fact]
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
                    .AddDependencyTo<AbstractThreshold, bool>("enabled", false)
            );

            // Act
            sut.Do();

            // Assert
            Container.Resolve<Concrete>().Should().BeOfType(A.Fake<Concrete>(options => options.WithArgumentsForConstructor(new List<object>() { 100, 200 })).GetType());
            Container.Resolve<Threshold>().Should().BeOfType(A.Fake<Threshold>(options => options.WithArgumentsForConstructor(new List<object>() { true })).GetType());
            Container.Resolve<PartialThreshold>().Should().BeOfType(A.Fake<PartialThreshold>(options => options.WithArgumentsForConstructor(new List<object>() { true })).GetType());
            Container.Resolve<AbstractThreshold>().Should().BeOfType(A.Fake<AbstractThreshold>(options => options.WithArgumentsForConstructor(new List<object>() { true })).GetType());
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
