using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using FluentAssertions;
using Tethos.NSubstitute.Tests.Attributes;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.FakeItEasy.Tests
{
    public class AutoFakeItEasyResolverTests
    {
        [Theory, AutoFakeItEasyData]
        public void MapToTarget_ShouldMatchMockedType(IMockable mockable, IKernel kernel, CreationContext creationContext)
        {
            // Arrange
            var sut = new AutoFakeItEasyResolver(kernel);
            var expected = mockable.GetType();
            var type = typeof(IMockable);

            // Act
            var actual = sut.MapToTarget(type, creationContext);

            // Assert
            actual.Should().BeOfType(expected);
        }
    }
}
