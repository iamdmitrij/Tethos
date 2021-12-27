namespace Tethos.Moq.Tests.AutoMockingTest
{
    using FluentAssertions;
    using global::Moq;
    using Tethos.Moq.Tests.Attributes;
    using Tethos.Tests.Common;
    using Xunit;

    public class ProxyObjectTests : Moq.AutoMockingTest
    {
        [Theory]
        [AutoMoqData]
        [Trait("Type", "Integration")]
        public void Resolve_ProxyObject_ShouldBeMock(Mock<IMockable> mock)
        {
            // Arrange
            var expected = mock.GetType();
            _ = this.Container.Resolve<SystemUnderTest>();
            var sut = this.Container.Resolve<IMockable>();

            // Act
            var actual = Mock.Get(sut);

            // Assert
            actual.Should().BeOfType(expected);
        }

        [Theory]
        [AutoMoqData]
        [Trait("Type", "Integration")]
        public void Resolve_ProxyObject_ShouldBeMockObject(Mock<IMockable> mock)
        {
            // Arrange
            var expected = mock.Object.GetType();
            _ = this.Container.Resolve<SystemUnderTest>();
            var sut = this.Container.Resolve<IMockable>();

            // Act
            var actual = Mock.Get(sut).Object;

            // Assert
            actual.Should().BeOfType(expected);
        }
    }
}
