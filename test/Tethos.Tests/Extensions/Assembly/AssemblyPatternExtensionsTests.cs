namespace Tethos.Tests.Extensions.Assembly;

using System;
using FluentAssertions;
using Tethos.Extensions.Assembly;
using Xunit;

public class AssemblyPatternExtensionsTests : BaseAutoMockingTest<AutoMockingContainer>
{
    [Theory]
    [InlineData(".,", "")]
    [InlineData("Tethos, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Tethos")]
    [InlineData("Tethos.Moq, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Tethos")]
    [InlineData("Tethos.Moq.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Tethos")]
    [InlineData("Tethos.Moq.Tests.Performance, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Tethos")]
    [InlineData("System.Data.SQLite, Version=1.0.66.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139", "System")]
    [InlineData("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", "mscorlib")]
    [Trait("Type", "Unit")]
    public void GetPattern_ShouldMatch(string assemblyName, string expected)
    {
        // Act
        var actual = assemblyName.GetPattern();

        // Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("FooBar")]
    [InlineData("Foo Bar")]
    [Trait("Type", "Unit")]
    public void GetPattern_ShouldThrowArgumentException(string assemblyName)
    {
        // Arrange
        var expected = "Could not determine pattern " +
                $@"for assembly named ""{assemblyName}"". Please use a different method for obtaining assemblies.";
        var actual = () => assemblyName.GetPattern();

        // Act & Assert
        actual.Should().Throw<ArgumentException>().And.Message.Should().Be(expected);
    }
}
