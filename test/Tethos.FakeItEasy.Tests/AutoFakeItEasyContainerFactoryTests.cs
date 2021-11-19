using FakeItEasy;
using FluentAssertions;
using Tethos.FakeItEasy.Tests.Attributes;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.FakeItEasy.Tests
{
    public class AutoFakeItEasyContainerFactoryTests
    {
        [Theory, FactoryContainerData]
        [Trait("", "Integration")]
        public void Create_SimpleDependency_ShouldMatchValue(
            IAutoFakeItEasyContainer container,
            int expected
        )
        {
            // Arrange
            var sut = container.Resolve<SystemUnderTest>();
            var mock = container.Resolve<IMockable>();
            A.CallTo(() => mock.Do()).Returns(expected);

            // Act
            var actual = sut.Do();

            // Assert
            actual.Should().Be(expected);
        }
    }
}
