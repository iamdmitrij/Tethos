﻿namespace Tethos.Tests
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Threading.Tasks;
    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;
    using FluentAssertions;
    using Moq;
    using Tethos.Tests.Attributes;
    using Tethos.Tests.SUT;
    using Xunit;

    public class AutoResolverTests
    {
        [Theory]
        [InlineAutoMoqData(typeof(IList), true)]
        [InlineAutoMoqData(typeof(IEnumerable), true)]
        [InlineAutoMoqData(typeof(Array), false)]
        [InlineAutoMoqData(typeof(Enumerable), false)]
        [InlineAutoMoqData(typeof(Type), false)]
        [InlineAutoMoqData(typeof(AutoResolver), false)]
        [InlineAutoMoqData(typeof(TimeoutException), false)]
        [InlineAutoMoqData(typeof(Guid), false)]
        [InlineAutoMoqData(typeof(Task<>), false)]
        [InlineAutoMoqData(typeof(Task<int>), false)]
        [InlineAutoMoqData(typeof(int), false)]
        [Trait("Category", "Unit")]
        public void CanResolve_ShouldMatch(
            Type type,
            bool expected,
            IKernel kernel,
            CreationContext resolver,
            string key)
        {
            // Arrange
            var sut = new ConcreteAutoResolver(kernel);

            // Act
            var actual = sut.CanResolve(
                resolver,
                resolver,
                new ComponentModel(),
                new DependencyModel(key, type, false));

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [AutoMoqData]
        [Trait("Category", "Unit")]
        public void Resolve_ShouldMatchMockType(
            Mock<IKernel> kernel,
            Mock<object> expected,
            CreationContext resolver,
            string key)
        {
            // Arrange
            var type = expected.GetType();
            kernel.Setup(mock => mock.Resolve(type)).Returns(expected);
            var sut = new ConcreteAutoResolver(kernel.Object);

            // Act
            var actual = sut.Resolve(
                resolver,
                resolver,
                new ComponentModel(),
                new DependencyModel(key, type, false));

            // Assert
            actual.Should().Equals(expected);
        }
    }
}
