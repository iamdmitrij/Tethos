using AutoFixture.Xunit2;
using Castle.MicroKernel;
using FluentAssertions;
using Moq;
using Xunit;

namespace Tethos.Moq.Tests
{
    public class AutoMoqResolverTests
    {
        [Fact]
        public void DiamondType_ShouldResolveToMoq()
        {
            // Arrange
            var sut = new AutoMoqResolver(new Mock<IKernel>().Object);
            var expected = typeof(Mock<>).GetType();

            // Act
            var actual = sut.DiamondType;

            // Assert
            actual.Should().BeOfType(expected);
        }

        [Theory, AutoData]
        public void MapToTarget_ShouldReturnMock(Mock<object> expected)
        {
            // Arrange
            var sut = new AutoMoqResolver(new Mock<IKernel>().Object);

            // Act
            var actual = sut.MapToTarget(expected);

            // Assert
            actual.Should().Equals(expected);
        }
    }
}
