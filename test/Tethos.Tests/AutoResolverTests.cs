namespace Tethos.Tests
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;
    using FluentAssertions;
    using Moq;
    using Tethos.Tests.SUT;
    using Xunit;

    public class AutoResolverTests
    {
        [Theory]
        [InlineAutoData(typeof(IList), true)]
        [InlineAutoData(typeof(IEnumerable), true)]
        [InlineAutoData(typeof(Array), false)]
        [InlineAutoData(typeof(Enumerable), false)]
        [InlineAutoData(typeof(Type), false)]
        [InlineAutoData(typeof(AutoResolver), false)]
        [InlineAutoData(typeof(TimeoutException), false)]
        [InlineAutoData(typeof(Guid), false)]
        [InlineAutoData(typeof(Task<>), false)]
        [InlineAutoData(typeof(Task<int>), false)]
        [InlineAutoData(typeof(int), false)]
        public void CanResolve_Interface_ShouldMatch(
            Type type,
            bool expected,
            Mock<IKernel> kernel,
            Mock<ISubDependencyResolver> resolver,
            string key
        )
        {
            // Arrange
            var sut = new ConcreteAutoResolver(kernel.Object);

            // Act
            var actual = sut.CanResolve(
                resolver.Object as CreationContext,
                resolver.Object,
                new ComponentModel(),
                new DependencyModel(key, type, false)
            );

            // Assert
            actual.Should().Be(expected);
        }

        [Theory, AutoData]
        public void Resolve_Object_ShouldMatch(
            Mock<IKernel> kernel,
            Mock<ISubDependencyResolver> resolver,
            string key
        )
        {
            // Arrange
            var expected = new Mock<object>(key);
            kernel.Setup(x => x.Resolve(typeof(Mock<object>))).Returns(expected);
            var sut = new ConcreteAutoResolver(kernel.Object);

            // Act
            var actual = sut.Resolve(
                resolver.Object as CreationContext,
                resolver.Object,
                new ComponentModel(),
                new DependencyModel(key, expected.GetType(), false)
            );

            // Assert
            actual.Should().Equals(expected);
        }
    }
}
