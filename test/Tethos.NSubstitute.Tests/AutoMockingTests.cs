namespace Tethos.NSubstitute.Tests
{
    using AutoFixture.Xunit2;
    using FluentAssertions;
    using global::NSubstitute;
    using Tethos.Tests.Common;
    using Xunit;

    public class AutoMockingTests
    {
        [Theory]
        [AutoData]
        [Trait("Type", "Integration")]
        [Trait("Type", "Thread")]
        public void SystemUnderTest_Exercise_ShouldMatch(int expected)
        {
            // Arrange
            var sut = AutoMocking.Container.Resolve<SystemUnderTest>();
            AutoMocking.Container.Resolve<IMockable>()
                .Get()
                .Returns(expected);

            // Act
            var actual = sut.Exercise();

            // Assert
            actual.Should().Be(expected);
            AutoMocking.Container.Resolve<IMockable>().Received().Get();
        }
    }
}
