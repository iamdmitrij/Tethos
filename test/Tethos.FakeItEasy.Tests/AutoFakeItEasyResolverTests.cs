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
        public void MapToTarget_ShouldMatchMockedType(IMockable mockable, IKernel kernel, Arguments constructorArguments)
        {
            // Arrange
            var sut = new AutoFakeItEasyResolver(kernel);
            var expected = mockable.GetType();
            var type = typeof(IMockable);

            // Act
            var actual = sut.MapToTarget(type, constructorArguments);

            // Assert
            actual.Should().BeOfType(expected);
        }
    }
}
