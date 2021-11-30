namespace ReferencedAssemblies.Tests
{
    using FluentAssertions;
    using Tethos;
    using Tethos.Tests.Common;
    using Xunit;

    public class BaseAutoMockingTestTests : BaseAutoMockingTest<AutoMockingContainer>
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void SystemUnderTest_Exercise_ShouldMatchValueRange()
        {
            // Arrange
            var sut = this.Container.Resolve<SystemUnderTest>();

            // Act
            var actual = sut.Exercise();

            // Assert
            actual.Should().BeInRange(0, 10);
        }
    }
}
