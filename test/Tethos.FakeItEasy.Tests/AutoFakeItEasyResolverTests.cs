using Castle.MicroKernel;
using FluentAssertions;
using System;
using Tethos.NSubstitute.Tests;
using Xunit;

namespace Tethos.FakeItEasy.Tests
{
    public class AutoFakeItEasyResolverTests
    {
        [Theory, AutoFakeItEasyData]
        public void DiamondType_ShouldBeNull(IKernel kernel)
        {
            // Arrange
            // TODO: Use FakeItEasy NuGet to inject?
            var sut = new AutoFakeItEasyResolver(kernel);

            // Act
            var actual = sut.DiamondType;

            // Assert
            actual.Should().BeNull();
        }

        [Theory, AutoFakeItEasyData]
        public void MapToTarget_ShouldReturnMock(object expected, IKernel kernel, Type targetType)
        {
            // Arrange
            var sut = new AutoFakeItEasyResolver(kernel);

            // Act
            var actual = sut.MapToTarget(expected, targetType);

            // Assert
            actual.Should().Equals(expected);
        }
    }
}
