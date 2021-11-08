using Castle.MicroKernel;
using FluentAssertions;
using Tethos.NSubstitute.Tests.Attributes;
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
            object[] constructorArguments
        )
        {
            // Arrange
            var sut = new AutoNSubstituteResolver(kernel);
            var expected = mockable.GetType();
            var type = typeof(IMockable);

            // Act
            var actual = sut.MapToTarget(type, constructorArguments);

            // Assert
            actual.Should().BeOfType(expected);
        }
    }
}
