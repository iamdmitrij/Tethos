namespace Tethos.Tests.Extensions.Assembly;

using System.IO;
using System.Reflection;
using AutoFixture.Xunit3;
using FluentAssertions;
using Tethos.Extensions.Assembly;
using Xunit;

public class FileExtensionsTests : BaseAutoMockingTest<AutoMockingContainer>
{
    [Fact]
    [Trait("Type", "Unit")]
    public void GetFile_FromExecutingAssembly_ShouldMatchLocation()
    {
        // Arrange
        var expected = Assembly.GetExecutingAssembly().Location;

        // Act
        var actual = expected.GetFile();

        // Assert
        actual.Path.Should().Be(expected);
    }

    [Theory]
    [AutoData]
    [Trait("Type", "Unit")]
    public void GetFiles_FromEmptyDirectory_ShouldBeEmpty(string name)
    {
        // Arrange
        var directory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), name);
        Directory.CreateDirectory(directory);

        // Act
        var actual = directory.GetFiles();

        // Assert
        actual.Should().BeEmpty();

        // Teardown
        Directory.Delete(directory);
    }

    [Theory]
    [AutoData]
    [Trait("Type", "Unit")]
    public void GetFiles_FromDirectoryWithOneFile_ShouldHaveOneFile(string name)
    {
        // Arrange
        var directory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), name);
        Directory.CreateDirectory(directory);
        string fileName = Path.GetRandomFileName();
        System.IO.File.Create(Path.Combine(directory, fileName)).Close();

        // Act
        var actual = directory.GetFiles();

        // Assert
        actual.Should().HaveCount(1);

        // Teardown
        Directory.Delete(directory, true);
    }

    [Theory]
    [AutoData]
    [Trait("Type", "Unit")]
    public void GetFiles_FromRecursiveDirectory_ShouldHaveMultipleFiles(string name, string extraDir)
    {
        // Arrange
        var directory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), name);
        Directory.CreateDirectory(directory);
        System.IO.File.Create(Path.Combine(directory, Path.GetRandomFileName())).Close();
        Directory.CreateDirectory(Path.Combine(directory, extraDir));
        System.IO.File.Create(Path.Combine(directory, extraDir, Path.GetRandomFileName())).Close();

        // Act
        var actual = directory.GetFiles();

        // Assert
        actual.Should().HaveCount(2);

        // Teardown
        Directory.Delete(directory, true);
    }
}
