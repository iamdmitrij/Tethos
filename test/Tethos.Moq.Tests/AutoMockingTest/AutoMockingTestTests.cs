namespace Tethos.Moq.Tests.AutoMockingTest
{
    using System;
    using AutoFixture.Xunit2;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using FluentAssertions;
    using global::Moq;
    using Tethos.Extensions;
    using Tethos.Moq.Tests.Attributes;
    using Tethos.Tests.Common;
    using Xunit;

    public class AutoMockingTestTests : Moq.AutoMockingTest
    {
        [Fact]
        [Trait("Type", "Integration")]
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
        [Trait("Type", "Integration")]
        public void Container_ShouldHaveAutoResolverInstalled()
        {
            // Assert
            this.AutoResolver.Should().BeOfType<AutoResolver>();
        }

        [Theory]
        [AutoData]
        [Trait("Type", "Integration")]
        public void SystemUnderTest_Exercise_ShouldMatch(int expected)
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
        [Trait("Type", "Integration")]
        public void Clean_ShouldRevertBackToOriginalBehavior(Mockable mockable)
        {
            // Arrange
            var sut = this.Container.Resolve<SystemUnderTest>();
            var action = () => this.Container.Resolve<SystemUnderTest>().Exercise();

            this.Container.Register(Component.For<SystemUnderTest>()
                .OverridesExistingRegistration()
                .DependsOn(Dependency.OnValue<IMockable>(mockable)));

            // Act
            this.Clean();
            sut.Exercise();

            // Assert
            action.Should().Throw<NotImplementedException>();
        }

        [Theory]
        [AutoMoqData]
        [Trait("Type", "Integration")]
        public void Install_ShouldRegister(Mock<IWindsorContainer> container, Mock<IConfigurationStore> store)
        {
            // Arrange
            var expected = Component.For(typeof(Mock<>));

            // Act
            this.Install(container.Object, store.Object);

            // Assert
            container.Verify(mock => mock.Register(expected), Times.Once);
        }
    }
}
