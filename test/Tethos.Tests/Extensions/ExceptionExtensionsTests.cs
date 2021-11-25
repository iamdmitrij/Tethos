namespace Tethos.Tests.Extensions.Extensions
{
    using System;
    using AutoFixture.Xunit2;
    using FluentAssertions;
    using Tethos.Extensions;
    using Xunit;

    public class ExceptionExtensionsTests
    {
        [Theory]
        [AutoData]
        [Trait("Category", "Unit")]
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
        [Trait("Category", "Unit")]
        public void SwallowExceptions_WhenTypeMatchExpected_ShouldReturnDefault()
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
        [Trait("Category", "Unit")]
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
        [Trait("Category", "Unit")]
        public void Throws_WhenFuncDoesNotThrow_ShouldBeFalse(object @object)
        {
            // Arrange
            var sut = () => @object;

            // Act
            var actual = sut.Throws();

            // Assert
            actual.Should().BeFalse();
        }

        [Theory]
        [InlineAutoData(false, null)]
        [InlineAutoData(false, typeof(NotImplementedException))]
        [InlineAutoData(false, typeof(ArgumentException), typeof(ArgumentException))]
        [InlineAutoData(false, typeof(NotImplementedException), typeof(Exception), typeof(ArgumentException))]
        [InlineAutoData(true, typeof(NullReferenceException), typeof(Exception), typeof(ArgumentException))]
        [InlineAutoData(true, typeof(NullReferenceException))]
        [InlineAutoData(true, typeof(NullReferenceException), typeof(NullReferenceException))]
        [InlineAutoData(true, typeof(NullReferenceException), typeof(ArgumentException))]
        [Trait("Category", "Unit")]
        public void Throws_WhenTypesDoNotMatch_ShouldThrowSameException(bool expected, params Type[] type)
        {
            // Arrange
            Func<object> sut = () => throw new NullReferenceException();

            // Act
            var actual = sut.Throws(type);

            // Assert
            actual.Should().Be(expected);
        }
    }
}
