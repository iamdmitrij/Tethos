namespace Tethos.Tests.Extensions
{
    using System.Reflection;
    using FluentAssertions;
    using Tethos.Extensions;
    using Xunit;

    public class FileExtensionsTests : BaseAutoMockingTest<AutoMockingContainer>
    {
        [Fact]
        [Trait("Category", "Unit")]
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
