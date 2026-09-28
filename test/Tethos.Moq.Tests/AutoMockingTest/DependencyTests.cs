namespace Tethos.Moq.Tests.AutoMockingTest;

using AutoFixture.Xunit3;
using Castle.MicroKernel;
using global::Moq;
using Shouldly;
using Tethos.Extensions;
using Tethos.Tests.Common;
using Xunit;

public class DependencyTests : Moq.AutoMockingTest
{
    [Fact]
    [Trait("Type", "Integration")]
    public void Container_Resolve_WithClassAndArguments_ShouldMockClass()
    {
        // Arrange
        var expectedType = new Mock<Concrete>(MockBehavior.Strict, 100, 200).GetType();
        var actual = this.Container.Resolve<SystemUnderTestClass>(
            new Arguments()
                .AddNamed("minValue", 100)
                .AddNamed("maxValue", 200));
        var mock = this.Container.Resolve<Mock<Concrete>>();

        // Act
        actual.Exercise();

        // Assert
        mock.ShouldBeOfType(expectedType);
        mock.Verify(m => m.Get(), Times.Once);
    }

    [Theory]
    [AutoData]
    [Trait("Type", "Integration")]
    public void Container_Resolve_WithClassAndPrimitiveType_ShouldMatchMockTypes(bool value)
    {
        // Arrange
        var expectedType = new Mock<Concrete>(MockBehavior.Strict, 100, 200).GetType();
        var expectedThresholdType = new Mock<Threshold>(MockBehavior.Strict, value).GetType();

        var actual = this.Container.Resolve<SystemUnderTwoClasses>(
            new Arguments()
                .AddDependencyTo<Concrete, int>("minValue", 100)
                .AddDependencyTo<Concrete, int>("maxValue", 200)
                .AddDependencyTo<Threshold, bool>("enabled", value));
        var mock = this.Container.Resolve<Mock<Concrete>>();
        var thresholdMock = this.Container.Resolve<Mock<Threshold>>();

        // Act
        actual.Exercise();

        // Assert
        mock.ShouldBeOfType(expectedType);
        thresholdMock.ShouldBeOfType(expectedThresholdType);
    }

    [Theory]
    [AutoData]
    [Trait("Type", "Integration")]
    public void Container_Resolve_WithAbstractClass_ShouldMatchMockTypes(bool value)
    {
        // Arrange
        var expected = new Mock<AbstractThreshold>(MockBehavior.Strict, value).GetType();
        var actual = this.Container.Resolve<SystemUnderAbstractClasses>(
            new Arguments()
                .AddDependencyTo<AbstractThreshold, bool>("enabled", value));

        // Act
        actual.Exercise();

        // Assert
        this.Container.Resolve<Mock<AbstractThreshold>>().ShouldBeOfType(expected);
    }

    [Theory]
    [AutoData]
    [Trait("Type", "Integration")]
    public void Container_Resolve_WithPartialClass_ShouldMatchMockTypes(bool value)
    {
        // Arrange
        var expected = new Mock<PartialThreshold>(MockBehavior.Strict, value).GetType();
        var sut = this.Container.Resolve<SystemUnderPartialClass>(
            new Arguments()
                .AddDependencyTo<PartialThreshold, bool>("enabled", value));

        // Act
        sut.Exercise();

        // Assert
        this.Container.Resolve<Mock<PartialThreshold>>().ShouldBeOfType(expected);
    }

    [Fact]
    [Trait("Type", "Integration")]
    public void Container_Resolve_WithMixedClasses_ShouldCallMock()
    {
        // Arrange
        var sut = this.Container.Resolve<SystemUnderMixedClasses>(
            new Arguments()
                .AddNamed("demo", 1)
                .AddTyped(new SealedConcrete())
                .AddDependencyTo<Concrete, int>("minValue", 100)
                .AddDependencyTo<Concrete, int>("maxValue", 200)
                .AddDependencyTo<Threshold, bool>("enabled", true)
                .AddDependencyTo<PartialThreshold, bool>("enabled", false)
                .AddDependencyTo<AbstractThreshold, bool>("enabled", false));

        // Act
        sut.Exercise();

        // Assert
        this.Container.Resolve<Mock<Concrete>>().ShouldBeOfType(new Mock<Concrete>(MockBehavior.Strict, 100, 200).GetType());
        this.Container.Resolve<Mock<Threshold>>().ShouldBeOfType(new Mock<Threshold>(MockBehavior.Strict, true).GetType());
        this.Container.Resolve<Mock<PartialThreshold>>().ShouldBeOfType(new Mock<PartialThreshold>(MockBehavior.Strict, true).GetType());
        this.Container.Resolve<Mock<AbstractThreshold>>().ShouldBeOfType(new Mock<AbstractThreshold>(MockBehavior.Strict, true).GetType());
    }
}
