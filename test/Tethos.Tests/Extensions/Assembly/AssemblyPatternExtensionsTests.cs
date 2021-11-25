﻿namespace Tethos.Tests.Extensions.Assembly
{
    using System;
    using System.Reflection;
    using AutoFixture.Xunit2;
    using FluentAssertions;
    using Tethos.Extensions.Assembly;
    using Xunit;

    public class AssemblyPatternExtensionsTests : BaseAutoMockingTest<AutoMockingContainer>
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void GetPattern_WithExecutingAssembly_ShouldMatchProperPattern()
        {
            // Arrange
            var expected = "Tethos";
            var assemblyName = Assembly.GetExecutingAssembly().FullName;

            // Act
            var actual = assemblyName.GetPattern();

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [AutoData]
        [Trait("Category", "Unit")]
        public void GetPattern_WithSystemAssembly_ShouldThrowArgumentException(AssemblyStub assembly)
        {
            // Arrange
            Action action = () => assembly.FullName.GetPattern();

            // Act & Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}
