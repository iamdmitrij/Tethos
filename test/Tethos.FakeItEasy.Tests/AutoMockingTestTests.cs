namespace Tethos.FakeItEasy.Tests
{
    using System;
    using AutoFixture.Xunit2;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Registration;
    using FluentAssertions;
    using global::FakeItEasy;
    using Tethos.Tests.Common;
    using Xunit;

    public class AutoMockingTestTests : AutoMockingTest
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void Container_ShouldHaveAutoResolverInstalled()
        {
            // Assert
            this.AutoResolver.Should().BeOfType<AutoFakeItEasyResolver>();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Test_Idempotency_ShouldMatchMocks()
        {
            // Arrange
            var expected = this.Container.ResolveFrom<SystemUnderTest, IMockable>();

            // Act
            var actual = this.Container.ResolveFrom<SystemUnderTest, IMockable>();

            // Assert
            actual.Should().BeSameAs(expected);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Test_Idempotency_ShouldMatchMockTypes()
        {
            // Arrange
            // TODO: What if this can be omitted?
            this.Container.Resolve<SystemUnderTest>();
            var expected = this.Container.Resolve<IMockable>();

            // Act
            var actual = this.Container.Resolve<IMockable>();

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [AutoData]
        [Trait("Category", "Integration")]
        public void Test_SimpleDependency_ShouldMatchValue(int expected)
        {
            // Arrange
            var sut = this.Container.Resolve<SystemUnderTest>();
            var mock = this.Container.Resolve<IMockable>();
            A.CallTo(() => mock.Do()).Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Container_Resolve_WithClass_ShouldMockClass()
        {
            // Arrange
            var sut = this.Container.Resolve<SystemUnderTestClass>();
            var expected = sut.Mockable.GetType();
            var actual = this.Container.Resolve<Concrete>();

            // Act
            sut.Do();

            // Assert
            actual.Should().BeOfType(expected);
            A.CallTo(() => actual.Do()).MustHaveHappened();
        }

        [Theory]
        [AutoData]
        [Trait("Category", "Integration")]
        public void Container_Resolve_WithClassAndArguments_ShouldMockClass(int minValue, int maxValue)
        {
            // Arrange
            var sut = this.Container.Resolve<SystemUnderTestClass>(
                new Arguments()
                    .AddDependencyTo<Concrete, int>(nameof(minValue), minValue)
                    .AddDependencyTo<Concrete, int>(nameof(maxValue), maxValue));
            var expected = sut.Mockable.GetType();
            var actual = this.Container.Resolve<Concrete>();

            // Act
            sut.Do();

            // Assert
            A.CallTo(() => actual.Do()).MustHaveHappened();
            actual.MinValue.Should().Be(minValue);
            actual.MaxValue.Should().Be(maxValue);
            actual.Should().BeOfType(expected);
        }

        [Theory]
        [AutoData]
        [Trait("Category", "Integration")]
        public void Container_Resolve_WithClassAndPrimitiveType_ShouldMatchMockTypes(
            int minValue,
            int maxValue,
            bool enabled)
        {
            // Arrange
            var sut = this.Container.Resolve<SystemUnderTwoClasses>(
                new Arguments()
                    .AddDependencyTo<Concrete, int>(nameof(minValue), minValue)
                    .AddDependencyTo<Concrete, int>(nameof(maxValue), maxValue)
                    .AddDependencyTo<Threshold, bool>(nameof(enabled), enabled));
            var expectedType = sut.Mockable.GetType();
            var expectedThresholdType = sut.Threshold.GetType();
            var mock = this.Container.Resolve<Concrete>();
            var thresholdMock = this.Container.Resolve<Threshold>();

            // Act
            sut.Do();

            // Assert
            mock.Should().BeOfType(expectedType);
            mock.MinValue.Should().Be(minValue);
            mock.MaxValue.Should().Be(maxValue);
            thresholdMock.Should().BeOfType(expectedThresholdType);
            thresholdMock.Enalbed.Should().Be(enabled);
        }

        [Theory]
        [AutoData]
        [Trait("Category", "Integration")]
        public void Container_Resolve_WithAbstractClass_ShouldMatchMockTypes(bool enabled)
        {
            // Arrange
            var sut = this.Container.Resolve<SystemUnderAbstractClasses>(
                new Arguments()
                    .AddDependencyTo<AbstractThreshold, bool>(nameof(enabled), enabled));
            var expected = sut.Threshold.GetType();
            var actual = this.Container.Resolve<AbstractThreshold>();

            // Act
            sut.Do();

            // Assert
            actual.Should().BeOfType(expected);
            actual.Enalbed.Should().Be(enabled);
        }

        [Theory]
        [AutoData]
        [Trait("Category", "Integration")]
        public void Container_Resolve_WithPartialClass_ShouldMatchMockTypes(bool enabled)
        {
            // Arrange
            var sut = this.Container.Resolve<SystemUnderPartialClass>(
                new Arguments()
                    .AddDependencyTo<PartialThreshold, bool>(nameof(enabled), enabled));
            var expected = sut.Threshold.GetType();
            var actual = this.Container.Resolve<PartialThreshold>();

            // Act
            sut.Do();

            // Assert
            actual.Should().BeOfType(expected);
            actual.Enalbed.Should().Be(enabled);
        }

        [Theory]
        [AutoData]
        [Trait("Category", "Integration")]
        public void Container_Resolve_WithMixedClasses_ShouldCallMock(
            int minValue,
            int maxValue,
            bool thresholdEnabled,
            bool partialThresholdEnabled,
            bool abstractThresholdEnabled)
        {
            // Arrange
            var sut = this.Container.Resolve<SystemUnderMixedClasses>(
                new Arguments()
                    .AddNamed("demo", 1)
                    .AddTyped(new SealedConcrete())
                    .AddDependencyTo<Concrete, int>(nameof(minValue), minValue)
                    .AddDependencyTo<Concrete, int>(nameof(maxValue), maxValue)
                    .AddDependencyTo<Threshold, bool>("enabled", thresholdEnabled)
                    .AddDependencyTo<PartialThreshold, bool>("enabled", partialThresholdEnabled)
                    .AddDependencyTo<AbstractThreshold, bool>("enabled", abstractThresholdEnabled));
            var concrete = this.Container.Resolve<Concrete>();
            var threshold = this.Container.Resolve<Threshold>();
            var partialThreshold = this.Container.Resolve<PartialThreshold>();
            var abstractThreshold = this.Container.Resolve<AbstractThreshold>();

            // Act
            sut.Do();

            // Assert
            concrete.Should().BeOfType(sut.Mockable.GetType());
            threshold.Should().BeOfType(sut.Threshold.GetType());
            partialThreshold.Should().BeOfType(sut.PartialThreshold.GetType());
            abstractThreshold.Should().BeOfType(sut.AbstractThreshold.GetType());

            concrete.MinValue.Should().Be(minValue);
            concrete.MaxValue.Should().Be(maxValue);
            threshold.Enalbed.Should().Be(thresholdEnabled);
            partialThreshold.Enalbed.Should().Be(partialThresholdEnabled);
            abstractThreshold.Enalbed.Should().Be(abstractThresholdEnabled);
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
