namespace Tethos.FakeItEasy.Tests
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Threading.Tasks;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;
    using Castle.MicroKernel.Registration;
    using FluentAssertions;
    using global::FakeItEasy;
    using Tethos.FakeItEasy.Tests.Attributes;
    using Tethos.Tests.Attributes;
    using Tethos.Tests.Common;
    using Xunit;

    public class AutoResolverTests
    {
        [Theory]
        [InlineAutoFakeItEasyData(typeof(IList), true)]
        [InlineAutoFakeItEasyData(typeof(IEnumerable), true)]
        [InlineAutoFakeItEasyData(typeof(Array), true)]
        [InlineAutoFakeItEasyData(typeof(Enumerable), true)]
        [InlineAutoFakeItEasyData(typeof(Type), true)]
        [InlineAutoFakeItEasyData(typeof(BaseAutoResolver), true)]
        [InlineAutoFakeItEasyData(typeof(TimeoutException), true)]
        [InlineAutoFakeItEasyData(typeof(Guid), false)]
        [InlineAutoFakeItEasyData(typeof(Task<>), true)]
        [InlineAutoFakeItEasyData(typeof(Task<int>), true)]
        [InlineAutoFakeItEasyData(typeof(int), false)]
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
                new(),
                new(key, type, false));

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [AutoFakeItEasyData]
        [Trait("Type", "Unit")]
        public void MapToMock_ShouldRegisterMock(IKernel kernel, object targetObject, IMockable mockable)
        {
            // Arrange
            var expected = mockable.GetType();
            var sut = new AutoResolver(kernel);
            var type = typeof(IMockable);

            MockMapping argument = new() { TargetType = type, TargetObject = targetObject };

            // Act
            var actual = sut.MapToMock(argument);

            // Assert
            A.CallTo(() => kernel.Register(A<IRegistration>._)).MustHaveHappenedOnceExactly();
            actual.Should().BeOfType(expected);
        }

        [Theory]
        [AutoFakeItEasyData]
        [Trait("Type", "Unit")]
        public void MapToMock_ShouldNotRegisterMock(IKernel kernel, IMockable mockable)
        {
            // Arrange
            var expected = mockable.GetType();
            var sut = new AutoResolver(kernel);
            var type = typeof(IMockable);

            MockMapping argument = new() { TargetType = type, TargetObject = mockable };

            // Act
            var actual = sut.MapToMock(argument);

            // Assert
            A.CallTo(() => kernel.Register(A<IRegistration>._)).MustNotHaveHappened();
            actual.Should().BeOfType(expected);
        }

        [Theory]
        [AutoFakeItEasyData]
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

            MockMapping argument = new() { TargetType = type, TargetObject = mockable, ConstructorArguments = constructorArguments };

            // Act
            var actual = sut.MapToMock(argument);

            // Assert
            A.CallTo(() => kernel.Register(A<IRegistration>._)).MustNotHaveHappened();
            actual.Should().BeOfType(expected);
        }
    }
}
