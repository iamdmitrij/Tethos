﻿using Castle.MicroKernel;
using FluentAssertions;
using Moq;
using System;
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
            var expected = mockable.Object.GetType();
            var sut = new AutoMoqResolver(kernel.Object);
            kernel.Setup(x => x.Resolve(mockable.GetType())).Returns(mockable);

            // Act
            var actual = sut.MapToTarget(typeof(IMockable));

            // Assert
            actual.Should().BeOfType(expected);
        }

        [Theory, AutoMoqData]
        public void MapToTarget_WhenMockIsNull_ShouldReturnNull(Mock<IKernel> kernel, Type type)
        {
            // Arrange
            var sut = new AutoMoqResolver(kernel.Object);
            kernel.Setup(x => x.Resolve(type)).Returns(null);

            // Act
            var actual = sut.MapToTarget(type);

            // Assert
            actual.Should().BeNull();
        }
    }
}
