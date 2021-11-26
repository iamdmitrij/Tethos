namespace Tethos.NSubstitute.Tests
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Threading.Tasks;
    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;
    using FluentAssertions;
    using Tethos.NSubstitute.Tests.Attributes;
    using Tethos.Tests.Attributes;
    using Tethos.Tests.Common;
    using Xunit;

    public class AutoNSubstituteResolverTests
    {
        [Theory]
        [InlineAutoNSubstituteData(typeof(IList), true)]
        [InlineAutoNSubstituteData(typeof(IEnumerable), true)]
        [InlineAutoNSubstituteData(typeof(Array), true)]
        [InlineAutoNSubstituteData(typeof(Enumerable), true)]
        [InlineAutoNSubstituteData(typeof(Type), true)]
        [InlineAutoNSubstituteData(typeof(AutoResolver), true)]
        [InlineAutoNSubstituteData(typeof(TimeoutException), true)]
        [InlineAutoNSubstituteData(typeof(Guid), false)]
        [InlineAutoNSubstituteData(typeof(Task<>), true)]
        [InlineAutoNSubstituteData(typeof(Task<int>), true)]
        [InlineAutoNSubstituteData(typeof(int), false)]
        [Trait("Category", "Unit")]
        public void CanResolve_Interface_ShouldMatch(
            Type type,
            bool expected,
            IKernel kernel,
            CreationContext resolver,
            string key)
        {
            // Arrange
            var sut = new AutoNSubstituteResolver(kernel);

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
        [AutoNSubstituteData]
        [Trait("Category", "Unit")]
        public void MapToMock_ShouldMatchMockedType(
            IMockable mockable,
            object targetObject,
            IKernel kernel,
            Arguments constructorArguments)
        {
            // Arrange
            var sut = new AutoNSubstituteResolver(kernel);
            var expected = mockable.GetType();
            var type = typeof(IMockable);

            // Act
            var actual = sut.MapToMock(type, targetObject, constructorArguments);

            // Assert
            actual.Should().BeOfType(expected);
        }
    }
}
