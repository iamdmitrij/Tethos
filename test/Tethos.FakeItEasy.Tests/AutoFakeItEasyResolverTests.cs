namespace Tethos.FakeItEasy.Tests
{
    using Castle.MicroKernel;
    using Castle.MicroKernel.Registration;
    using FluentAssertions;
    using global::FakeItEasy;
    using Tethos.FakeItEasy.Tests.Attributes;
    using Tethos.Tests.Common;
    using Xunit;

    public class AutoFakeItEasyResolverTests
    {
        [Theory]
        [AutoFakeItEasyData]
        [Trait("Category", "Unit")]
        public void MapToMock_ShouldRegisterMock(IKernel kernel, object targetObject, IMockable mockable, Arguments constructorArguments)
        {
            // Arrange
            var expected = mockable.GetType();
            var sut = new AutoFakeItEasyResolver(kernel);
            var type = typeof(IMockable);

            // Act
            var actual = sut.MapToMock(type, targetObject, constructorArguments);

            // Assert
            A.CallTo(() => kernel.Register(A<IRegistration>._)).MustHaveHappenedOnceExactly();
            actual.Should().BeOfType(expected);
        }

        [Theory]
        [AutoFakeItEasyData]
        [Trait("Category", "Unit")]
        public void MapToMock_ShouldNotRegisterMock(IKernel kernel, IMockable mockable, Arguments constructorArguments)
        {
            // Arrange
            var expected = mockable.GetType();
            var sut = new AutoFakeItEasyResolver(kernel);
            var type = typeof(IMockable);

            // Act
            var actual = sut.MapToMock(type, mockable, constructorArguments);

            // Assert
            A.CallTo(() => kernel.Register(A<IRegistration>._)).MustNotHaveHappened();
            actual.Should().BeOfType(expected);
        }
    }
}
