namespace Tethos.FakeItEasy.Tests.AutoMockingTest;

using AutoFixture.Xunit3;
using Castle.MicroKernel;
using FluentAssertions;
using global::FakeItEasy;
using Tethos.Extensions;
using Tethos.Tests.Common;
using Xunit;

public class DependencyTests : FakeItEasy.AutoMockingTest
{
    [Fact]
    [Trait("Type", "Integration")]
    public void Container_Resolve_WithClass_ShouldMockClass()
    {
        // Arrange
        var sut = this.Container.Resolve<SystemUnderTestClass>();
        var expected = sut.Mockable.GetType();
        var actual = this.Container.Resolve<Concrete>();

        // Act
        sut.Exercise();

        // Assert
        actual.Should().BeOfType(expected);
        A.CallTo(() => actual.Get()).MustHaveHappened();
    }

    [Theory]
    [AutoData]
    [Trait("Type", "Integration")]
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
        sut.Exercise();

        // Assert
        A.CallTo(() => actual.Get()).MustHaveHappened();
        actual.MinValue.Should().Be(minValue);
        actual.MaxValue.Should().Be(maxValue);
        actual.Should().BeOfType(expected);
    }

    [Theory]
    [AutoData]
    [Trait("Type", "Integration")]
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
        sut.Exercise();

        // Assert
        mock.Should().BeOfType(expectedType);
        mock.MinValue.Should().Be(minValue);
        mock.MaxValue.Should().Be(maxValue);
        thresholdMock.Should().BeOfType(expectedThresholdType);
        thresholdMock.Enabled.Should().Be(enabled);
    }

    [Theory]
    [AutoData]
    [Trait("Type", "Integration")]
    public void Container_Resolve_WithAbstractClass_ShouldMatchMockTypes(bool enabled)
    {
        // Arrange
        var sut = this.Container.Resolve<SystemUnderAbstractClasses>(
            new Arguments()
                .AddDependencyTo<AbstractThreshold, bool>(nameof(enabled), enabled));
        var expected = sut.Threshold.GetType();
        var actual = this.Container.Resolve<AbstractThreshold>();

        // Act
        sut.Exercise();

        // Assert
        actual.Should().BeOfType(expected);
        actual.Enabled.Should().Be(enabled);
    }

    [Theory]
    [AutoData]
    [Trait("Type", "Integration")]
    public void Container_Resolve_WithPartialClass_ShouldMatchMockTypes(bool enabled)
    {
        // Arrange
        var sut = this.Container.Resolve<SystemUnderPartialClass>(
            new Arguments()
                .AddDependencyTo<PartialThreshold, bool>(nameof(enabled), enabled));
        var expected = sut.Threshold.GetType();
        var actual = this.Container.Resolve<PartialThreshold>();

        // Act
        sut.Exercise();

        // Assert
        actual.Should().BeOfType(expected);
        actual.Enabled.Should().Be(enabled);
    }

    [Theory]
    [AutoData]
    [Trait("Type", "Integration")]
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
        sut.Exercise();

        // Assert
        concrete.Should().BeOfType(sut.Mockable.GetType());
        threshold.Should().BeOfType(sut.Threshold.GetType());
        partialThreshold.Should().BeOfType(sut.PartialThreshold.GetType());
        abstractThreshold.Should().BeOfType(sut.AbstractThreshold.GetType());

        concrete.MinValue.Should().Be(minValue);
        concrete.MaxValue.Should().Be(maxValue);
        threshold.Enabled.Should().Be(thresholdEnabled);
        partialThreshold.Enabled.Should().Be(partialThresholdEnabled);
        abstractThreshold.Enabled.Should().Be(abstractThresholdEnabled);
    }
}
