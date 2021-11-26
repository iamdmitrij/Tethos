namespace Tethos.NSubstitute.Tests
{
    using Castle.MicroKernel;
    using Castle.MicroKernel.Registration;
    using FluentAssertions;
    using global::NSubstitute;
    using Tethos.NSubstitute.Tests.Attributes;
    using Tethos.Tests.Common;
    using Xunit;

    public class AutoNSubstituteResolverTests
    {
        [Theory]
        [AutoNSubstituteData]
        [Trait("Category", "Unit")]
        public void MapToMock_WithPlainObject_ShouldRegisterMock(IKernel kernel, object targetObject, IMockable mockable, Arguments constructorArguments)
        {
            // Arrange
            var expected = mockable.GetType();
            var sut = new AutoNSubstituteResolver(kernel);
            var type = typeof(IMockable);

            // Act
            var actual = sut.MapToMock(type, targetObject, constructorArguments);

            // Assert
            kernel.Received().Register(Arg.Any<IRegistration>());
            actual.Should().BeOfType(expected);
        }

        [Theory]
        [AutoNSubstituteData]
        [Trait("Category", "Unit")]
        public void MapToMock_WithMockedObject_ShouldNotRegisterMock(IKernel kernel, IMockable mockable, Arguments constructorArguments)
        {
            // Arrange
            var expected = mockable.GetType();
            var sut = new AutoNSubstituteResolver(kernel);
            var type = typeof(IMockable);

            // Act
            var actual = sut.MapToMock(type, mockable, constructorArguments);

            // Assert
            kernel.DidNotReceive().Register(Arg.Any<IRegistration>());
            actual.Should().BeOfType(expected);
        }
    }
}
