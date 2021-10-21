using Castle.MicroKernel;
using FluentAssertions;
using Tethos.NSubstitute.Tests;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.FakeItEasy.Tests
{
    public class AutoFakeItEasyResolverTests
    {
        [Theory, AutoFakeItEasyData]
        public void MapToTarget_ShouldMatchMockedType(IMockable mockable, IKernel kernel)
        {
            // Arrange
            var sut = new AutoFakeItEasyResolver(kernel);
            var expected = mockable.GetType();
            var type = typeof(IMockable);

            // Act
            var actual = sut.MapToTarget(type);

            // Assert
            actual.Should().BeOfType(expected);
        }
    }
}
