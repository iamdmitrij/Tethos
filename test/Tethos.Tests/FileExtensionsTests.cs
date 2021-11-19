using FluentAssertions;
using System.Reflection;
using Xunit;

namespace Tethos.Tests
{
    public class FileExtensionsTests : BaseAutoMockingTest<AutoMockingContainer>
    {
        [Fact]
        [Trait("Category", "Internal")]
        public void GetPath_FromExecutingAssembly_ShouldMatchLocation()
        {
            // Arrange
            var assembly = Assembly.GetExecutingAssembly();
            var expected = assembly.Location;

            // Act
            var actual = expected.GetFile();

            // Assert
            actual.Path.Should().Be(expected);
        }
    }
}
