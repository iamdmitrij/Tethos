using Castle.MicroKernel;
using FluentAssertions;
using Moq;
using System;
using Tethos.Moq.Tests.Attributes;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.Moq.Tests
{
    public class AutoMoqResolverTests
    {
        [Theory, AutoMoqData]
        public void MapToTarget_ShouldMatchMockedType(Mock<IKernel> kernel, Mock<IMockable> mockable, Arguments constructorArguments)
        {
            // Arrange
            var expected = mockable.Object.GetType();
            var sut = new AutoMoqResolver(kernel.Object);
            kernel.Setup(x => x.Resolve(mockable.GetType())).Returns(mockable);

            // Act
            var actual = sut.MapToTarget(typeof(IMockable), constructorArguments);

            // Assert
            actual.Should().BeOfType(expected);
        }
    }
}
