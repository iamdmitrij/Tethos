﻿namespace Tethos.Tests
{
    using System;
    using AutoFixture.Xunit2;
    using Castle.MicroKernel.Registration;
    using FluentAssertions;
    using Xunit;

    public class WindsorExtensionsTests
    {
        [Fact]
        public void OverridesExistingRegistration_PassNull_ShouldBeNull()
        {
            // Arrange
            ComponentRegistration sut = null;

            // Act
            var actual = sut.OverridesExistingRegistration();

            // Assert
            actual.Should().BeNull();
        }

        [Theory, AutoData]
        public void OverridesExistingRegistration_ShouldSetNameToGuid(ComponentRegistration sut)
        {
            // Act
            var actual = sut.OverridesExistingRegistration();

            // Assert
            Guid.TryParseExact(actual.Name, "D", out _).Should().BeTrue();
        }
    }
}
