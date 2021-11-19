using FluentAssertions;
using NSubstitute;
using Tethos.NSubstitute.Tests.Attributes;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.NSubstitute.Tests
{
    public class AutoNSubstituteContainerFactoryTests
    {
        [Theory, FactoryContainerData]
        [Trait("Category", "Integration")]
        public void Create_SimpleDependency_ShouldMatchValue(
            IAutoNSubstituteContainer container,
            int expected
        )
        {
            // Arrange
            var sut = container.Resolve<SystemUnderTest>();
            container.Resolve<IMockable>()
                .Do()
                .Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            actual.Should().Be(expected);
        }
    }
}
