namespace Tethos.Tests.Extensions.Assembly
{
    using System.IO;
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
            var expected = Assembly.GetExecutingAssembly().Location;

            // Act
            var actual = expected.GetFile();

            // Assert
            actual.Path.Should().Be(expected);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetAssemblyFiles_FromCurrentDirectory_ShouldNotBeEmpty()
        {
            // Arrange
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Act
            var actual = directory.GetAssemblyFiles();

            // Assert
            actual.Should().NotBeEmpty();
        }
    }
}
