namespace Tethos.NSubstitute.Tests
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Threading.Tasks;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;
    using Castle.MicroKernel.Registration;
    using FluentAssertions;
    using global::NSubstitute;
    using Tethos.NSubstitute.Tests.Attributes;
    using Tethos.Tests.Attributes;
    using Tethos.Tests.Common;
    using Xunit;

    public class AutoResolverTests
    {
        [Theory]
        [InlineAutoNSubstituteData(typeof(IList), true)]
        [InlineAutoNSubstituteData(typeof(IEnumerable), true)]
        [InlineAutoNSubstituteData(typeof(Array), true)]
        [InlineAutoNSubstituteData(typeof(Enumerable), true)]
        [InlineAutoNSubstituteData(typeof(Type), true)]
        [InlineAutoNSubstituteData(typeof(BaseAutoResolver), true)]
        [InlineAutoNSubstituteData(typeof(TimeoutException), true)]
        [InlineAutoNSubstituteData(typeof(Guid), false)]
        [InlineAutoNSubstituteData(typeof(Task<>), true)]
        [InlineAutoNSubstituteData(typeof(Task<int>), true)]
        [InlineAutoNSubstituteData(typeof(int), false)]
        [Trait("Type", "Unit")]
        public void CanResolve_ShouldMatch(
            Type type,
            bool expected,
            IKernel kernel,
            CreationContext resolver,
            string key)
        {
            // Arrange
            var sut = new AutoResolver(kernel);

            // Act
            var actual = sut.CanResolve(
                resolver,
                resolver,
                new (),
                new (key, type, false));

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [AutoNSubstituteData]
        [Trait("Type", "Unit")]
        public void MapToMock_ShouldRegisterMock(IKernel kernel, object targetObject, IMockable mockable)
        {
            // Arrange
            var expected = mockable.GetType();
            var sut = new AutoResolver(kernel);
            var type = typeof(IMockable);

            MockMapping argument = new () { TargetType = type, TargetObject = targetObject };

            // Act
            var actual = sut.MapToMock(argument);

            // Assert
            kernel.Received(1).Register(Arg.Any<IRegistration>());
            actual.Should().BeOfType(expected);
        }

        [Theory]
        [AutoNSubstituteData]
        [Trait("Type", "Unit")]
        public void MapToMock_ShouldNotRegisterMock(IKernel kernel, IMockable mockable)
        {
            // Arrange
            var expected = mockable.GetType();
            var sut = new AutoResolver(kernel);
            var type = typeof(IMockable);

            MockMapping argument = new () { TargetType = type, TargetObject = mockable };

            // Act
            var actual = sut.MapToMock(argument);

            // Assert
            kernel.DidNotReceive().Register(Arg.Any<IRegistration>());
            actual.Should().BeOfType(expected);
        }

        [Theory]
        [AutoNSubstituteData]
        [Trait("Type", "Unit")]
        public void MapToMock_WithConstructorArguments_ShouldMatchMockType(IKernel kernel, IMockable mockable)
        {
            // Arrange
            var expected = mockable.GetType();
            var sut = new AutoResolver(kernel);
            var type = typeof(Concrete);
            var constructorArguments = new Arguments()
                .AddNamed("minValue", 100)
                .AddNamed("maxValue", 200);

            MockMapping argument = new () { TargetType = type, TargetObject = mockable, ConstructorArguments = constructorArguments };

            // Act
            var actual = sut.MapToMock(argument);

            // Assert
            kernel.DidNotReceive().Register(Arg.Any<IRegistration>());
            actual.Should().BeOfType(expected);
        }
    }
}
