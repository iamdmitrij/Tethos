namespace Tethos.Moq.Tests
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Threading.Tasks;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;
    using Castle.MicroKernel.Registration;
    using FluentAssertions;
    using global::Moq;
    using Tethos.Moq.Tests.Attributes;
    using Tethos.Tests.Common;
    using Xunit;

    public class AutoResolverTests
    {
        [Theory]
        [InlineAutoMoqData(typeof(IList), 1, true)]
        [InlineAutoMoqData(typeof(IEnumerable), 2, true)]
        [InlineAutoMoqData(typeof(Array), 5, true)]
        [InlineAutoMoqData(typeof(Enumerable), 15, true)]
        [InlineAutoMoqData(typeof(Type), 4, true)]
        [InlineAutoMoqData(typeof(Tethos.AutoResolver), 8, true)]
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
        public void CanResolve_ShouldMatch(
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
            var sut = new AutoResolver(kernel);

            // Act
            var actual = sut.CanResolve(
                resolver,
                resolver,
                new(),
                new(key, type, false));

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [AutoMoqData]
        [Trait("Category", "Unit")]
        public void MapToMock_ShouldRegisterMock(Mock<IKernel> kernel, object targetObject, IMockable mockable, Arguments constructorArguments)
        {
            // Arrange
            var expected = mockable.GetType();
            var sut = new AutoResolver(kernel.Object);
            var type = typeof(IMockable);

            // Act
            var actual = sut.MapToMock(type, targetObject, constructorArguments);

            // Assert
            kernel.Verify(m => m.Register(It.IsAny<IRegistration>()), Times.AtLeastOnce);
            actual.Should().BeOfType(expected);
        }

        [Theory]
        [AutoMoqData]
        [Trait("Category", "Unit")]
        public void MapToMock_ShouldNotRegisterMock(Mock<IKernel> kernel, IMockable mockable, Arguments constructorArguments)
        {
            // Arrange
            var expected = mockable.GetType();
            var sut = new AutoResolver(kernel.Object);
            var type = typeof(IMockable);

            // Act
            var actual = sut.MapToMock(type, mockable, constructorArguments);

            // Assert
            kernel.Verify(m => m.Register(It.IsAny<IRegistration>()), Times.Never);
            actual.Should().BeOfType(expected);
        }

        [Theory]
        [AutoMoqData]
        [Trait("Category", "Unit")]
        public void MapToMock_WithConstructorArguments_ShouldMatchMockType(Mock<IKernel> kernel, Mock<Concrete> mockable)
        {
            // Arrange
            var expected = mockable.Object.GetType();
            var sut = new AutoResolver(kernel.Object);
            var type = typeof(Concrete);
            var arguments = new Arguments()
                .AddNamed("minValue", 100)
                .AddNamed("maxValue", 200);

            // Act
            var actual = sut.MapToMock(type, mockable.Object, arguments);

            // Assert
            kernel.Verify(m => m.Register(It.IsAny<IRegistration>()), Times.Never);
            actual.Should().BeOfType(expected);
        }
    }
}
