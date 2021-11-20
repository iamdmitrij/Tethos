using FluentAssertions;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.Tests
{
    public class BaseAutoMockingTestTests : BaseAutoMockingTest<AutoMockingContainer>
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void SystemUnderTest_Do_ShouldMatchValueRange()
        {
            // Arrange
            var sut = this.Container.Resolve<SystemUnderTest>();

            // Act
            var actual = sut.Do();

            // Assert
            actual.Should().BeInRange(0, 10);
        }
    }
}
