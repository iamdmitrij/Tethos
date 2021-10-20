using AutoFixture.Xunit2;
using Castle.MicroKernel;
using FakeItEasy;
using FluentAssertions;
using System;
using Xunit;

namespace Tethos.FakeItEasy.Tests
{
    public class AutoFakeItEasyResolverTests
    {
        [Theory, AutoData]
        public void DiamondType_ShouldResolveToMoqType(Fake<IKernel> kernel)
        {
            // Arrange
            var sut = new AutoFakeItEasyResolver(kernel.FakedObject);
            var expected = typeof(Fake<>).GetType();

            // Act
            var actual = sut.DiamondType;

            // Assert
            actual.Should().BeOfType(expected);
        }

        [Theory, AutoData]
        public void MapToTarget_ShouldReturnMock(Fake<object> expected, Fake<IKernel> kernel, Type targetType)
        {
            // Arrange
            var sut = new AutoFakeItEasyResolver(kernel.FakedObject);

            // Act
            var actual = sut.MapToTarget(expected, targetType);

            // Assert
            actual.Should().Equals(expected);
        }
    }
}
