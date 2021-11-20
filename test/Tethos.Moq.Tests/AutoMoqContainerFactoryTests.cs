using FluentAssertions;
using Moq;
using Tethos.Moq;
using Tethos.Moq.Tests.Attributes;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.FakeItEasy.Tests
{
    public class AutoMoqContainerFactoryTests
    {
        [Theory, FactoryContainerData]
        [Trait("Category", "Integration")]
        public void Create_SimpleDependency_ShouldMatchValue(
            IAutoMoqContainer container,
            int expected)
        {
            // Arrange
            var sut = container.Resolve<SystemUnderTest>();

            container.Resolve<Mock<IMockable>>()
                .Setup(x => x.Do())
                .Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            actual.Should().Be(expected);
        }
    }
}
