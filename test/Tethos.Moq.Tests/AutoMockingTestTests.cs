using AutoFixture.Xunit2;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using FluentAssertions;
using Moq;
using System;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.Moq.Tests
{
    public class AutoMockingTestTests : AutoMockingTest
    {
        [Fact]
        public void Container_ShouldHaveMockInstalled()
        {
            // Arrange
            var expected = typeof(Mock<object>);

            // Act
            var actual = Container.Resolve(expected);

            // Assert
            actual.Should().BeOfType(expected);
        }

        [Fact]
        public void Container_ShouldHaveAutoResolverInstalled()
        {
            // Act
            var actual = Container.Resolve<ISubDependencyResolver>();

            // Assert
            actual.Should().BeOfType<AutoMoqResolver>();
        }

        [Theory, AutoData]
        public void Test_SimpleDependency_ShouldMatchValue(int expected)
        {
            // Arrange
            var sut = Container.Resolve<SystemUnderTest>();

            Container.Resolve<Mock<IMockable>>()
                .Setup(x => x.Do())
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
            var expectedType = new Mock<Concrete>(100, 200).GetType();
            var actual = Container.Resolve<SystemUnderTestClass>(
                new Arguments()
                    .AddNamed("minValue", 100)
                    .AddNamed("maxValue", 200)
            );
            var mock = Container.Resolve<Mock<Concrete>>();

            // Act
            actual.Do();

            // Assert
            mock.Should().BeOfType(expectedType);
            mock.Verify(x => x.Do(), Times.Once);
        }

        [Theory, AutoData]
        public void Container_Resolve_WithClassAndPrimitiveType_ShouldMatchMockTypes(bool value)
        {
            // Arrange
            var expectedType = new Mock<Concrete>(100, 200).GetType();
            var expectedThresholdType = new Mock<Threshold>(value).GetType();

            var actual = Container.Resolve<SystemUnderTwoClasses>(
                new Arguments()
                    .AddDependencyTo<Concrete, int>("minValue", 100)
                    .AddDependencyTo<Concrete, int>("maxValue", 200)
                    .AddDependencyTo<Threshold, bool>("enabled", value)
            );
            var mock = Container.Resolve<Mock<Concrete>>();
            var thresholdMock = Container.Resolve<Mock<Threshold>>();

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
            var expected = new Mock<AbstractThreshold>(value).GetType();
            var actual = Container.Resolve<SystemUnderAbstractClasses>(
                new Arguments()
                    .AddDependencyTo<AbstractThreshold, bool>("enabled", value)
            );

            // Act
            actual.Do();

            // Assert
            Container.Resolve<Mock<AbstractThreshold>>().Should().BeOfType(expected);
        }

        [Theory, AutoData]
        public void Container_Resolve_WithPartialClass_ShouldMatchMockTypes(bool value)
        {
            // Arrange
            var expected = new Mock<PartialThreshold>(value).GetType();
            var sut = Container.Resolve<SystemUnderPartialClass>(
                new Arguments()
                    .AddDependencyTo<PartialThreshold, bool>("enabled", value)
            );

            // Act
            sut.Do();

            // Assert
            Container.Resolve<Mock<PartialThreshold>>().Should().BeOfType(expected);
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
            Container.Resolve<Mock<Concrete>>().Should().BeOfType(new Mock<Concrete>(100, 200).GetType()).GetType();
            Container.Resolve<Mock<Threshold>>().Should().BeOfType(new Mock<Threshold>(true).GetType()).GetType();
            Container.Resolve<Mock<PartialThreshold>>().Should().BeOfType(new Mock<PartialThreshold>(true).GetType()).GetType();
            Container.Resolve<Mock<AbstractThreshold>>().Should().BeOfType(new Mock<AbstractThreshold>(true).GetType()).GetType();
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
