namespace Tethos.NSubstitute.Tests
{
    using AutoFixture.Xunit2;
    using FluentAssertions;
    using global::FakeItEasy;
    using Tethos.Tests.Common;
    using Xunit;
    using AutoMocking = Tethos.FakeItEasy.AutoMocking;

    public class AutoMockingTests
    {
        [Theory]
        [AutoData]
        [Trait("Type", "Integration")]
        public void SystemUnderTest_Exercise_ShouldMatch(int expected)
        {
            // Arrange
            var sut = AutoMocking.Container.Resolve<SystemUnderTest>();

            global::FakeItEasy.A.CallTo(() => AutoMocking.Container.Resolve<IMockable>().Get()).Returns(expected);

            // Act
            var actual = sut.Exercise();

            // Assert
            actual.Should().Be(expected);
            global::FakeItEasy.A.CallTo(() => AutoMocking.Container.Resolve<IMockable>().Get()).MustHaveHappened();
        }
    }
}
