using AutoFixture.Xunit2;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using FakeItEasy;
using FluentAssertions;
using System;
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
        public void Clean_ShouldRevertBackToOriginalBehavior()
        {
            // Arrange
            var sut = Container.Resolve<SystemUnderTest>();

            Container.Register(Component.For<SystemUnderTest>()
                .OverridesExistingRegistration()
                .DependsOn(Dependency.OnValue<IMockable>(new Mockable()))
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
