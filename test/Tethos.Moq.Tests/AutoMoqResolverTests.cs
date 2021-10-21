using Castle.MicroKernel;
using FluentAssertions;
using Moq;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.Moq.Tests
{
    public class AutoMoqResolverTests
    {
        [Fact]
        public void MapToTarget_ShouldReturnMock()
        {
            // Arrange
            var kernel = new Mock<IKernel>();
            var expected = new Mock<IMockable>();

            kernel.Setup(x => x.Resolve(typeof(Mock<IMockable>))).Returns(expected);
            var sut = new AutoMoqResolver(kernel.Object);
            // Act
            var actual = sut.MapToTarget(typeof(IMockable));

            // Assert
            actual.Should().BeOfType(expected.Object.GetType());
        }
    }
}
