namespace Tethos.Tests.Extensions.Assembly
{
    using System.Reflection;
    using FluentAssertions;
    using Tethos.Extensions.Assembly;
    using Xunit;

    public class FileExtensionsTests : BaseAutoMockingTest<AutoMockingContainer>
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void GetFile_FromExecutingAssembly_ShouldMatchLocation()
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
