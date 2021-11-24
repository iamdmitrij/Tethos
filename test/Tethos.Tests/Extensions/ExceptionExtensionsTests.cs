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
    }
}
