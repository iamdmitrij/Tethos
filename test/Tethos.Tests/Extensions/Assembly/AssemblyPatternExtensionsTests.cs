namespace Tethos.Tests.Extensions.Assembly
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
            var assemblyName = assembly.FullName;
            var expected = "Could not determine application name " +
                    $"for assembly {assemblyName}. Please use a different method for obtaining assemblies.";
            var action = () => assemblyName.GetPattern();

            // Act & Assert
            action.Should().Throw<ArgumentException>().And.Message.Should().Be(expected);
        }
    }
}
