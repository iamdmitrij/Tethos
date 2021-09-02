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

            // Act
            var actual = sut.DiamondType;

            // Assert
            actual.GetType();
            actual.Should().BeOfType(typeof(Mock<>).GetType());
        }

        [Theory, AutoData]
        public void MapToTarget_ShouldReturnMock(Mock<object> mock)
        {
            // Arrange
            var sut = new AutoMoqResolver(new Mock<IKernel>().Object);

            // Act
            var actual = sut.MapToTarget(mock);

            // Assert
            actual.Should().Equals(mock);
        }
    }
}
