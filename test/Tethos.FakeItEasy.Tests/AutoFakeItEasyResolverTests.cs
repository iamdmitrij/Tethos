namespace Tethos.FakeItEasy.Tests
{
    using Castle.MicroKernel;
    using FluentAssertions;
    using Tethos.FakeItEasy.Tests.Attributes;
    using Tethos.Tests.Common;
    using Xunit;

    public class AutoFakeItEasyResolverTests
    {
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
