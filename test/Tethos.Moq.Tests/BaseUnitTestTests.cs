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
    public class BaseUnitTestTests : AutoMockingTest
    {
        [Fact]
        public void Container_ShouldHaveMockInstalled()
        {
            // Arrange
            var expectedType = typeof(Mock<object>);

            // Assert
            Container.Resolve(expectedType).Should().NotBeNull();
        }

        [Fact]
        public void Container_ShouldHaveAutoResolverInstalled()
        {
            // Assert
            Container.Resolve<ISubDependencyResolver>().Should().BeOfType<AutoMoqResolver>();
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
        public void CleanAutomockingContainer_ShouldInjectConcreteClass(Guid containerKey)
        {
            // Arrange
            var sut = Container.Resolve<SystemUnderTest>();

            Container.Register(Component.For<SystemUnderTest>()
                .Named(containerKey.ToString())
                .IsDefault()
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
