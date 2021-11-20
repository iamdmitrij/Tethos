using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using FluentAssertions;
using Moq;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Tethos.Tests.Attributes;
using Tethos.Tests.SUT;
using Xunit;

namespace Tethos.Tests
{
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
        public void CanResolve_Interface_ShouldMatch(
            Type type,
            bool expected,
            IKernel kernel,
            ISubDependencyResolver resolver,
            CreationContext context,
            string key)
        {
            // Arrange
            var sut = new ConcreteAutoResolver(kernel);

            // Act
            var actual = sut.CanResolve(
                context,
                resolver,
                new ComponentModel(),
                new DependencyModel(key, type, false));

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [AutoMoqData]
        [Trait("Category", "Unit")]
        public void Resolve_Object_ShouldMatch(
            Mock<IKernel> kernel,
            ISubDependencyResolver resolver,
            CreationContext context,
            string key)
        {
            // Arrange
            var expected = new Mock<object>(key);
            kernel.Setup(x => x.Resolve(typeof(Mock<object>))).Returns(expected);
            var sut = new ConcreteAutoResolver(kernel.Object);

            // Act
            var actual = sut.Resolve(
                context,
                resolver,
                new ComponentModel(),
                new DependencyModel(key, expected.GetType(), false));

            // Assert
            actual.Should().Equals(expected);
        }
    }
}
