using FluentAssertions;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.Tests
{
    public class BaseAutoMockingTestTests : BaseAutoMockingTest<AutoMockingContainer>
    {
        [Fact]
        public void SystemUnderTest_Do_ShouldMatchValueRange()
        {
            // Arrange
            var sut = Container.Resolve<SystemUnderTest>();

            // Act
            var actual = sut.Do();

            // Assert
            actual.Should().BeInRange(0, 10);
        }
    }
}
