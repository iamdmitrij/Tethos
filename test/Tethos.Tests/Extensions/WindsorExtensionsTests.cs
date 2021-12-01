namespace Tethos.Tests.Extensions
{
    using System;
    using AutoFixture.Xunit2;
    using Castle.MicroKernel.Registration;
    using FluentAssertions;
    using Tethos.Extensions;
    using Xunit;

    public class WindsorExtensionsTests
    {
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
