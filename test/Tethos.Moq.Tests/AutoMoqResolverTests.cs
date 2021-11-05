using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using FluentAssertions;
using Moq;
using System;
using Tethos.NSubstitute.Tests.Attributes;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.Moq.Tests
{
    public class AutoMoqResolverTests
    {
        [Theory, AutoMoqData]
        public void MapToTarget_ShouldMatchMockedType(Mock<IKernel> kernel, Mock<IMockable> mockable, Mock<CreationContext> creationContext)
        {
            // Arrange
            var expected = mockable.Object.GetType();
            var sut = new AutoMoqResolver(kernel.Object);
            kernel.Setup(x => x.Resolve(mockable.GetType())).Returns(mockable);

            // Act
            var actual = sut.MapToTarget(typeof(IMockable), creationContext.Object);

            // Assert
            actual.Should().BeOfType(expected);
        }

        [Theory, AutoMoqData]
        public void MapToTarget_WhenMockIsNull_ShouldReturnNull(Mock<IKernel> kernel, Type type, Mock<CreationContext> creationContext)
        {
            // Arrange
            var sut = new AutoMoqResolver(kernel.Object);
            kernel.Setup(x => x.Resolve(type)).Returns(null);

            // Act
            var actual = sut.MapToTarget(type, creationContext.Object);

            // Assert
            actual.Should().BeNull();
        }
    }
}
