namespace Tethos.Moq.Tests
{
    using Castle.MicroKernel;
    using Castle.MicroKernel.Registration;
    using FluentAssertions;
    using global::Moq;
    using Tethos.Moq.Tests.Attributes;
    using Tethos.Tests.Common;
    using Xunit;

    public class AutoMoqResolverTests
    {
        [Theory]
        [AutoMoqData]
        [Trait("Category", "Unit")]
        public void MapToMock_ShouldRegisterMock(Mock<IKernel> kernel, object targetObject, IMockable mockable, Arguments constructorArguments)
        {
            // Arrange
            var expected = mockable.GetType();
            var sut = new AutoMoqResolver(kernel.Object);
            var type = typeof(IMockable);

            // Act
            var actual = sut.MapToMock(type, targetObject, constructorArguments);

            // Assert
            kernel.Verify(m => m.Register(It.IsAny<IRegistration>()), Times.Once);
            actual.Should().BeOfType(expected);
        }

        [Theory]
        [AutoMoqData]
        [Trait("Category", "Unit")]
        public void MapToMock_ShouldNotRegisterMock(Mock<IKernel> kernel, IMockable mockable, Arguments constructorArguments)
        {
            // Arrange
            var expected = mockable.GetType();
            var sut = new AutoMoqResolver(kernel.Object);
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
            var sut = new AutoMoqResolver(kernel.Object);
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
