using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using FluentAssertions;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.NSubstitute.Tests
{
    public class AutoNSubstituteResolverTests
    {
        [Theory, AutoNSubstituteData]
        public void MapToTarget_ShouldMatchMockedType(
            IMockable mockable,
            IKernel kernel,
            CreationContext resolver
        )
        {
            // Arrange
            var sut = new AutoNSubstituteResolver(kernel);
            var expected = mockable.GetType();
            var type = typeof(IMockable);

            // Act
            var actual = sut.MapToTarget(type, resolver);

            // Assert
            actual.Should().BeOfType(expected);
        }
    }
}
