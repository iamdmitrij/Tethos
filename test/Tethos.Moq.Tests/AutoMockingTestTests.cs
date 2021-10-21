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
