namespace Tethos.NSubstitute.Tests
{
    using AutoFixture.Xunit2;
    using FluentAssertions;
    using global::NSubstitute;
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
            A.Container.Resolve<IMockable>()
                .Get()
                .Returns(expected);

            // Act
            var actual = sut.Exercise();

            // Assert
            actual.Should().Be(expected);
            A.Container.Resolve<IMockable>().Received().Get();
        }
    }
}
