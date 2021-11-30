namespace Tethos.FakeItEasy.Tests
{
    using FluentAssertions;
    using global::FakeItEasy;
    using Tethos.FakeItEasy.Tests.Attributes;
    using Tethos.Tests.Common;
    using Xunit;

    public class AutoMockingContainerFactoryTests
    {
        [Theory]
        [FactoryContainerData]
        [Trait("Category", "Integration")]
        public void Create_SimpleDependency_ShouldMatchValue(
            IAutoMockingContainer container,
            int expected)
        {
            // Arrange
            var sut = container.Resolve<SystemUnderTest>();
            var mock = container.Resolve<IMockable>();
            A.CallTo(() => mock.Get()).Returns(expected);

            // Act
            var actual = sut.Exercise();

            // Assert
            actual.Should().Be(expected);
        }
    }
}
