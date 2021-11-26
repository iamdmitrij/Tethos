namespace Tethos.Moq.Tests
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Threading.Tasks;
    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;
    using FluentAssertions;
    using global::Moq;
    using Tethos.Moq.Tests.Attributes;
    using Tethos.Tests.Common;
    using Xunit;

    public class AutoMoqResolverTests
    {
        [Theory]
        [InlineAutoMoqData(typeof(IList), 1, true)]
        [InlineAutoMoqData(typeof(IEnumerable), 2, true)]
        [InlineAutoMoqData(typeof(Array), 5, true)]
        [InlineAutoMoqData(typeof(Enumerable), 15, true)]
        [InlineAutoMoqData(typeof(Type), 4, true)]
        [InlineAutoMoqData(typeof(AutoResolver), 8, true)]
        [InlineAutoMoqData(typeof(TimeoutException), 10, true)]
        [InlineAutoMoqData(typeof(Guid), 2, false)]
        [InlineAutoMoqData(typeof(Task<>), 4, true)]
        [InlineAutoMoqData(typeof(Task<int>), 10, true)]
        [InlineAutoMoqData(typeof(int), 8, false)]
        [InlineAutoMoqData(typeof(IList), 0, true)]
        [InlineAutoMoqData(typeof(Array), 0, false)]
        [InlineAutoMoqData(typeof(Guid), 0, false)]
        [InlineAutoMoqData(typeof(int), 0, false)]
        [Trait("Category", "Unit")]
        public void CanResolve_Interface_ShouldMatch(
            Type type,
            int arguments,
            bool expected,
            IKernel kernel,
            CreationContext resolver,
            string key)
        {
            // Arrange
            Enumerable.Range(0, arguments)
                .Select(_ => new Arguments().AddNamed($"{Guid.NewGuid()}", Guid.NewGuid()))
                .ToList()
                .ForEach(argument => resolver.AdditionalArguments.Add(argument));
            var sut = new AutoMoqResolver(kernel);

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
        public void MapToMock_ShouldMatchMockedType(Mock<IKernel> kernel, object targetObject, Mock<IMockable> mockable, Arguments constructorArguments)
        {
            // Arrange
            var expected = mockable.Object.GetType();
            var sut = new AutoMoqResolver(kernel.Object);
            kernel.Setup(mock => mock.Resolve(mockable.GetType())).Returns(mockable);

            // Act
            var actual = sut.MapToMock(typeof(IMockable), targetObject, constructorArguments);

            // Assert
            actual.Should().BeOfType(expected);
        }
    }
}
