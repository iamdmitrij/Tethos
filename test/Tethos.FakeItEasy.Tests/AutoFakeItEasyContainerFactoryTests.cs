namespace Tethos.FakeItEasy.Tests
{
    using FakeItEasy;
    using FluentAssertions;
    using Tethos.FakeItEasy.Tests.Attributes;
    using Tethos.Tests.Common;
    using Xunit;

    public class AutoFakeItEasyContainerFactoryTests
    {
        [Theory, FactoryContainerData]
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
