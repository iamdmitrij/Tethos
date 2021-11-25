namespace Tethos.Moq.Tests
{
    using Castle.MicroKernel;
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
        public void MapToMock_ShouldMatchMockedType(Mock<IKernel> kernel, object targetObject, Mock<IMockable> mockable, Arguments constructorArguments)
        {
            // Arrange
            var expected = mockable.Object.GetType();
            var sut = new AutoMoqResolver(kernel.Object);
            kernel.Setup(x => x.Resolve(mockable.GetType())).Returns(mockable);

            // Act
            var actual = sut.MapToMock(typeof(IMockable), targetObject, constructorArguments);

            // Assert
            actual.Should().BeOfType(expected);
        }
    }
}
