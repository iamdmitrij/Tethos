namespace Tethos.Tests.Extensions.Assembly
{
    using System.Linq;
    using System.Reflection;
    using FluentAssertions;
    using Tethos.Extensions.Assembly;
    using Xunit;

    public class AssemblyFilteringExtensionsTests : BaseAutoMockingTest<AutoMockingContainer>
    {
        [Theory]
        [InlineData("", 0, "")]
        [InlineData("Fake", 0, "")]
        [InlineData("Fake", 2, "Fake.Core31.dll", "Fake.Core31.pdb", "Fake.Core31.exe")]
        [InlineData("Fake", 0, "Fake.Standard20.deps.json")]
        [InlineData("Fake", 1, "Fake.Standard20.dll")]
        [InlineData("Tethos", 1, "ref/Tethos.Tests.dll")]
        [InlineData("Tethos", 2, "ref/Tethos.Tests.dll", "Tethos.Tests.Common.dll")]
        [InlineData("Tethos", 0, "Fake.Standard20.dll")]
        [InlineData("Tethos", 1, "Fake.Standard20.dll", "ref/Tethos.Tests.dll")]
        [InlineData("xunit", 1, "xunit.abstractions.dll")]
        [Trait("Type", "Unit")]
        public void FilterAssemblies_ShouldMatchCount(string pattern, int expected, params string[] assemblies)
        {
            // Arrange
            var extensions = new[] { ".dll", ".exe" };
            var files = assemblies.Select(assembly => assembly.GetFile());

            // Act
            var actual = files.FilterAssemblies(pattern, extensions);

            // Assert
            actual.Should().HaveCount(expected);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("Fake", false)]
        [InlineData("Fake.Net60.exe", false, "Fake.Net60.dll")]
        [InlineData("Fake.Net60.exe", false, "Fake.Core31.dll", "Fake.Core30.dll")]
        [InlineData("Fake.Core30.dll", true, "Fake.Core31.dll", "Fake.Core30.dll")]
        [InlineData("Fake.Net60.dll", true, "Fake.Net60.dll")]
        [InlineData("xunit.abstractions.dll", true, "xunit.abstractions.dll")]
        [Trait("Type", "Unit")]
        public void ContainsAssemblyNamed_ShouldMatch(string name, bool expected, params string[] assemblies)
        {
            // Act
            var actual = assemblies
                .Select(Assembly.LoadFrom)
                .ContainsAssemblyNamed(name);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(0, "ref/Fake.Core31.dll", "foo/ref/Fake.Core31.dll")]
        [InlineData(0, "ref/Fake.Core31.ref.dll")]
        [InlineData(1, "Fake.Core31.dll")]
        [InlineData(2, "Fake.ref.Core31.dll", "Fake.ref.ref.ref")]
        [Trait("Type", "Unit")]
        public void ExcludeRefDirectory(int expected, params string[] assemblies)
        {
            // Arrange
            var files = assemblies.Select(assembly => assembly.GetFile());

            // Act
            var actual = files.ExcludeRefDirectory();

            // Assert
            actual.Should().HaveCount(expected);
        }
    }
}
