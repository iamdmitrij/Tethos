namespace Tethos.Tests.Extensions
{
    using System;
    using AutoFixture.Xunit2;
    using Castle.MicroKernel.Registration;
    using FluentAssertions;
    using Tethos.Extensions;
    using Tethos.Tests.Attributes;
    using Xunit;

    public class WindsorExtensionsTests
    {
        [Theory]
        [AutoMoqData]
        [Trait("Category", "Unit")]
        public void IncludeNonPublicTypes_ShouldMatch(FromAssemblyDescriptorStub expected, AutoMockingConfiguration configuration)
        {
            // Act
            var actual = expected.IncludeNonPublicTypes(configuration) as FromAssemblyDescriptorStub;

            // Assert
            actual.Should().BeSameAs(expected);
        }

        [Theory]
        [AutoMoqData]
        [Trait("Category", "Unit")]
        public void IncludeNonPublicTypes_ExcludeNonPublicTypes_ShouldBeFalse(FromAssemblyDescriptorStub expected, AutoMockingConfiguration configuration)
        {
            // Arrange
            configuration.IncludeNonPublicTypes = false;

            // Act
            var actual = expected.IncludeNonPublicTypes(configuration) as FromAssemblyDescriptorStub;

            // Assert
            actual.Should().Be(expected);
            actual.IncludesNonPublicTypes.Should().BeFalse();
        }

        [Theory]
        [AutoMoqData]
        [Trait("Category", "Unit")]
        public void IncludeNonPublicTypes_WithIncludeNonPublicTypes_ShouldBeTrue(FromAssemblyDescriptorStub expected, AutoMockingConfiguration configuration)
        {
            // Arrange
            configuration.IncludeNonPublicTypes = true;

            // Act
            var actual = expected.IncludeNonPublicTypes(configuration) as FromAssemblyDescriptorStub;

            // Assert
            actual.Should().Be(expected);
            actual.IncludesNonPublicTypes.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", "Unit")]
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
        [Trait("Category", "Unit")]
        public void OverridesExistingRegistration_ShouldHaveGuidInTheNamed(ComponentRegistration sut)
        {
            // Act
            var actual = sut.OverridesExistingRegistration();

            // Assert
            Guid.TryParseExact(actual.Name, "D", out _).Should().BeTrue();
        }
    }
}
