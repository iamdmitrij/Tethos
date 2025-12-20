namespace Tethos.Tests.Extensions.Extensions;

using System;
using AutoFixture.Xunit3;
using FluentAssertions;
using Tethos.Extensions;
using Xunit;

public class ExceptionExtensionsTests
{
    [Theory]
    [AutoData]
    [Trait("Type", "Unit")]
    public void SwallowExceptions_WithNoExceptions_ShouldMatch(object expected)
    {
        // Arrange
        var sut = () => expected;

        // Act
        var actual = sut.SwallowExceptions();

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    [Trait("Type", "Unit")]
    public void SwallowExceptions_WhenTypeMatchExpected_ShouldMatchDefault()
    {
        // Arrange
        var expected = default(object);
        Func<object> sut = () => throw new NotImplementedException();

        // Act
        var actual = sut.SwallowExceptions(typeof(NotImplementedException));

        // Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineAutoData(null)]
    [InlineAutoData(typeof(NotImplementedException))]
    [InlineAutoData(typeof(ArgumentException), typeof(ArgumentException))]
    [InlineAutoData(typeof(NotImplementedException), typeof(Exception), typeof(ArgumentException))]
    [Trait("Type", "Unit")]
    public void SwallowExceptions_WhenTypesDoNotMatch_ShouldThrowSameException(params Type[] type)
    {
        // Arrange
        Func<object> sut = () => throw new NullReferenceException();

        // Act
        var actual = () => sut.SwallowExceptions(type);

        // Assert
        actual.Should().Throw<NullReferenceException>();
    }

    [Theory]
    [AutoData]
    [Trait("Type", "Unit")]
    public void Throws_WhenFuncDoesNotThrow_ShouldBeFalse(object @object)
    {
        // Arrange
        var sut = () => @object;

        // Act
        var actual = sut.Throws();

        // Assert
        actual.Should().BeFalse();
    }

    /// <summary>
    /// TODO: Open an issue, because I cannot use params here for type:
    /// https://github.com/AutoFixture/AutoFixture.xUnit3/blob/48af207c92dbd79335751fbbaa667ddec9cb584b/Src/AutoFixture.xUnit3/Internal/InlineDataSource.cs#L42
    /// </summary>
    [Theory]
    [InlineAutoData(false, null, null)]
    [InlineAutoData(false, typeof(NotImplementedException), null)]
    [InlineAutoData(false, typeof(ArgumentException), typeof(ArgumentException))]
    [InlineAutoData(false, typeof(NotImplementedException), typeof(ArgumentException))]
    [InlineAutoData(true, typeof(NullReferenceException), typeof(ArgumentException))]
    [InlineAutoData(true, typeof(NullReferenceException), null)]
    [InlineAutoData(true, typeof(NullReferenceException), typeof(NullReferenceException))]
    [InlineAutoData(true, typeof(NullReferenceException), typeof(ArgumentException))]
    [Trait("Type", "Unit")]
    public void Throws_WhenTypesDoNotMatch_ShouldMatch(bool expected, Type type, Type innerType)
    {
        // Arrange
        Func<object> sut = () => throw new NullReferenceException();

        // Act
        var actual = sut.Throws(type, innerType);

        // Assert
        actual.Should().Be(expected);
    }
}
