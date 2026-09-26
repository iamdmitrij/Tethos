namespace Tethos.Tests.Extensions;

using System;
using AutoFixture.Xunit3;
using Castle.MicroKernel.Registration;
using FluentAssertions;
using Tethos.Extensions;
using Tethos.Tests.Attributes;
using Xunit;

public class WindsorExtensionsTests
{
    [Theory]
    [AutoMoqData]
    [Trait("Type", "Unit")]
    public void IncludeNonPublicTypes_ShouldMatch(FromAssemblyDescriptorStub expected, AutoMockingConfiguration configuration)
    {
        // Act
        var actual = expected.IncludeNonPublicTypes(configuration) as FromAssemblyDescriptorStub;

        // Assert
        actual.Should().BeSameAs(expected);
    }

    [Theory]
    [AutoMoqData]
    [Trait("Type", "Unit")]
    public void IncludeNonPublicTypes_ExcludeNonPublicTypes_ShouldBeFalse(FromAssemblyDescriptorStub descriptor, AutoMockingConfiguration configuration)
    {
        // Arrange
        var expected = false;
        configuration.IncludeNonPublicTypes = expected;

        // Act
        var actual = descriptor.IncludeNonPublicTypes(configuration) as FromAssemblyDescriptorStub;

        // Assert
        actual.IncludesNonPublicTypes.Should().Be(expected);
    }

    [Theory]
    [AutoMoqData]
    [Trait("Type", "Unit")]
    public void IncludeNonPublicTypes_WithIncludeNonPublicTypes_ShouldBeTrue(FromAssemblyDescriptorStub descriptor, AutoMockingConfiguration configuration)
    {
        // Arrange
        var expected = true;
        configuration.IncludeNonPublicTypes = expected;

        // Act
        var actual = descriptor.IncludeNonPublicTypes(configuration) as FromAssemblyDescriptorStub;

        // Assert
        actual.IncludesNonPublicTypes.Should().Be(expected);
    }

    [Fact]
    [Trait("Type", "Unit")]
    public void OverridesExistingRegistration_PassNull_ShouldBeNull()
    {
        // Arrange
        ComponentRegistration sut = null;

        // Act
        var actual = sut.OverridesExistingRegistration();

        // Assert
        actual.Should().BeNull();
    }

    [Theory]
    [AutoData]
    [Trait("Type", "Unit")]
    public void OverridesExistingRegistration_ShouldHaveGuidInTheNamed(ComponentRegistration sut)
    {
        // Act
        var registration = sut.OverridesExistingRegistration();
        var actual = Guid.TryParseExact(registration.Name, "D", out _);

        // Assert
        actual.Should().BeTrue();
    }
}
