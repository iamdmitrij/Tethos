namespace Tethos.FakeItEasy.Tests
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Threading.Tasks;
    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;
    using FluentAssertions;
    using Tethos.FakeItEasy.Tests.Attributes;
    using Tethos.Tests.Attributes;
    using Tethos.Tests.Common;
    using Xunit;

    public class AutoFakeItEasyResolverTests
    {
        [Theory]
        [InlineAutoFakeItEasyData(typeof(IList), true)]
        [InlineAutoFakeItEasyData(typeof(IEnumerable), true)]
        [InlineAutoFakeItEasyData(typeof(Array), true)]
        [InlineAutoFakeItEasyData(typeof(Enumerable), true)]
        [InlineAutoFakeItEasyData(typeof(Type), true)]
        [InlineAutoFakeItEasyData(typeof(AutoResolver), true)]
        [InlineAutoFakeItEasyData(typeof(TimeoutException), true)]
        [InlineAutoFakeItEasyData(typeof(Guid), false)]
        [InlineAutoFakeItEasyData(typeof(Task<>), true)]
        [InlineAutoFakeItEasyData(typeof(Task<int>), true)]
        [InlineAutoFakeItEasyData(typeof(int), false)]
        [Trait("Category", "Unit")]
        public void CanResolve_ShouldMatch(
            Type type,
            bool expected,
            IKernel kernel,
            CreationContext resolver,
            string key)
        {
            // Arrange
            var sut = new AutoFakeItEasyResolver(kernel);

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
        [AutoFakeItEasyData]
        [Trait("Category", "Unit")]
        public void MapToMock_ShouldMatchMockedType(IMockable mockable, object targetObject, IKernel kernel, Arguments constructorArguments)
        {
            // Arrange
            var sut = new AutoFakeItEasyResolver(kernel);
            var expected = mockable.GetType();
            var type = typeof(IMockable);

            // Act
            var actual = sut.MapToMock(type, targetObject, constructorArguments);

            // Assert
            actual.Should().BeOfType(expected);
        }
    }
}
