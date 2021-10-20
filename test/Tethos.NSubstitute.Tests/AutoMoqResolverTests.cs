using AutoFixture.Xunit2;
using Castle.MicroKernel;
using FluentAssertions;
using NSubstitute;
using System;
using Xunit;

namespace Tethos.NSubstitute.Tests
{
    public class AutoMoqResolverTests
    {
        [Fact]
        public void DiamondType_ShouldBeNull()
        {
            // Arrange
            // TODO: Use AutoNSubstitute NuGet to inject?
            var kernel = Substitute.For<IKernel>();
            var sut = new AutoNSubstituteResolver(kernel);

            // Act
            var actual = sut.DiamondType;

            // Assert
            actual.Should().BeNull();
        }

        [Theory, AutoData]
        public void MapToTarget_ShouldReturnMock(Type targetType)
        {
            // Arrange
            // TODO: Use AutoNSubstitute NuGet to inject?
            var expected = Substitute.For<object>();
            var kernel = Substitute.For<IKernel>();
            var sut = new AutoNSubstituteResolver(kernel);

            // Act
            var actual = sut.MapToTarget(expected, targetType);

            // Assert
            actual.Should().Equals(expected);
        }
    }
}
