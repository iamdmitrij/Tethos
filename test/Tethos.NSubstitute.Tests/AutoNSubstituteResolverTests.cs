using Castle.MicroKernel;
using FluentAssertions;
using System;
using Xunit;

namespace Tethos.NSubstitute.Tests
{
    public class AutoNSubstituteResolverTests
    {
        [Theory, AutoNSubstituteData]
        public void DiamondType_ShouldBeNull(IKernel kernel)
        {
            // Arrange
            var sut = new AutoNSubstituteResolver(kernel);

            // Act
            var actual = sut.DiamondType;

            // Assert
            actual.Should().BeNull();
        }

        [Theory, AutoNSubstituteData]
        public void MapToTarget_ShouldReturnMock(
            object expected,
            IKernel kernel,
            Type targetType
        )
        {
            // Arrange
            var sut = new AutoNSubstituteResolver(kernel);

            // Act
            var actual = sut.MapToTarget(expected, targetType);

            // Assert
            actual.Should().Equals(expected);
        }
    }
}
