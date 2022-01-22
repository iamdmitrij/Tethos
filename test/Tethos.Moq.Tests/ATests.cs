namespace Tethos.FakeItEasy.Tests
{
    using AutoFixture.Xunit2;
    using FluentAssertions;
    using global::Moq;
    using Tethos.Moq;
    using Tethos.Tests.Common;
    using Xunit;

    public class ATests
    {
        [Theory]
        [AutoData]
        [Trait("Type", "Integration")]
        public void SystemUnderTest_Exercise_ShouldMatch(int expected)
        {
            // Arrange
            var sut = A.Container.Resolve<SystemUnderTest>();

            A.Container.Resolve<Mock<IMockable>>()
                .Setup(mock => mock.Get())
                .Returns(expected);

            // Act
            var actual = sut.Exercise();

            // Assert
            actual.Should().Be(expected);
            A.Container.Resolve<Mock<IMockable>>().Verify();
        }
    }
}
