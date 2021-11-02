namespace Tethos.NSubstitute.Tests
{
    using Castle.MicroKernel;
    using FluentAssertions;
    using Tethos.NSubstitute.Tests.Attributes;
    using Tethos.Tests.Common;
    using Xunit;

    public class AutoNSubstituteResolverTests
    {
        [Theory, AutoNSubstituteData]
        public void MapToTarget_ShouldMatchMockedType(
            IMockable mockable,
            IKernel kernel
        )
        {
            // Arrange
            var sut = new AutoNSubstituteResolver(kernel);
            var expected = mockable.GetType();
            var type = typeof(IMockable);

            // Act
            var actual = sut.MapToTarget(type);

            // Assert
            actual.Should().BeOfType(expected);
        }
    }
}
