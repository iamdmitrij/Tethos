namespace Tethos.Tests.Extensions.Extensions;

using System;
using AutoFixture.Xunit3;
using Shouldly;
using Tethos.Extensions;
using Xunit;

public class ExceptionExtensionsTests
{
    public static TheoryData<Type[]> ExceptionTypeData =>
        new()
        {
            { null! },
            { new[] { typeof(NotImplementedException) } },
            { new[] { typeof(ArgumentException), typeof(ArgumentException) } },
            { new[] { typeof(NotImplementedException), typeof(Exception), typeof(ArgumentException) } },
        };

    public static TheoryData<bool, Type[]> ExpectedAndExceptionTypeData =>
        new()
        {
            { false, null! },
            { false, new[] { typeof(NotImplementedException) } },
            { false, new[] { typeof(ArgumentException), typeof(ArgumentException) } },
            { false, new[] { typeof(NotImplementedException), typeof(Exception), typeof(ArgumentException) } },
            { true, new[] { typeof(NullReferenceException), typeof(Exception), typeof(ArgumentException) } },
            { true, new[] { typeof(NullReferenceException) } },
            { true, new[] { typeof(NullReferenceException), typeof(NullReferenceException) } },
            { true, new[] { typeof(NullReferenceException), typeof(ArgumentException) } },
        };

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
        actual.ShouldBe(expected);
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
        actual.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(ExceptionTypeData))]
    [Trait("Type", "Unit")]
    public void SwallowExceptions_WhenTypesDoNotMatch_ShouldThrowSameException(Type[] type)
    {
        // Arrange
        Func<object> sut = () => throw new NullReferenceException();

        // Act
        var actual = () => sut.SwallowExceptions(type);

        // Assert
        actual.ShouldThrow<NullReferenceException>();
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
        actual.ShouldBeFalse();
    }

    [Theory]
    [MemberData(nameof(ExpectedAndExceptionTypeData))]
    [Trait("Type", "Unit")]
    public void Throws_WhenTypesDoNotMatch_ShouldMatch(bool expected, Type[] type)
    {
        // Arrange
        Func<object> sut = () => throw new NullReferenceException();

        // Act
        var actual = sut.Throws(type);

        // Assert
        actual.ShouldBe(expected);
    }
}
