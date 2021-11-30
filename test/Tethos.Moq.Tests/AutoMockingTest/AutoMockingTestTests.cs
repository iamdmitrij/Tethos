namespace Tethos.Moq.Tests.AutoMockingTest
{
    using System;
    using AutoFixture.Xunit2;
    using Castle.MicroKernel.Registration;
    using FluentAssertions;
    using global::Moq;
    using Tethos.Extensions;
    using Tethos.Tests.Common;
    using Xunit;

    public class AutoMockingTestTests : Moq.AutoMockingTest
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void Container_ShouldHaveMockInstalled()
        {
            // Arrange
            var expected = typeof(Mock<object>);

            // Act
            var actual = this.Container.Resolve(expected);

            // Assert
            actual.Should().BeOfType(expected);
        }

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

            this.Container.Resolve<Mock<IMockable>>()
                .Setup(mock => mock.Get())
                .Returns(expected);

            // Act
            var actual = sut.Exercise();

            // Assert
            actual.Should().Be(expected);
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
            Action action = () => concrete.Exercise();
            sut.Exercise();

            // Assert
            action.Should().Throw<NotImplementedException>();
        }
    }
}
