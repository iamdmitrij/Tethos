using Castle.MicroKernel;
using FluentAssertions;
using Moq;
using Tethos.NSubstitute.Tests;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.Moq.Tests
{
    public class AutoMoqResolverTests
    {
        [Theory, AutoMoqData]
        public void MapToTarget_ShouldMatchMockedType(Mock<IKernel> kernel, Mock<IMockable> mockable)
        {
            // Arrange
            kernel.Setup(x => x.Resolve(mockable.GetType())).Returns(mockable);
            var expected = mockable.Object.GetType();
            var sut = new AutoMoqResolver(kernel.Object);

            // Act
            var actual = sut.MapToTarget(typeof(IMockable));

            // Assert
            actual.Should().BeOfType(expected);
        }
    }
}
