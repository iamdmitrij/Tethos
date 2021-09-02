using FluentAssertions;
using Tethos.Tests.Common;
using Xunit;

namespace Tethos.Tests
{
    public class BaseTestTests : BaseAutoMockingTest<AutoMockingContainer>
    {
        [Fact]
        public void Test_SimplyDependency_ShouldMatchValue()
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
