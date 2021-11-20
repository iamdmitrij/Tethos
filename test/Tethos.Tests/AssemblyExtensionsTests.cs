namespace Tethos.Tests
{
    using AutoFixture.Xunit2;
    using FluentAssertions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Xunit;

    public class AssemblyExtensionsTests : BaseAutoMockingTest<AutoMockingContainer>
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void TryToLoadAssembly_WithCorruptAssembly_ShouldReturnNull()
        {
            // Arrange
            var corruptAssembly = "Fake.Core30.exe";

            // Act
            var actual = corruptAssembly.TryToLoadAssembly();

            // Assert
            actual.Should().BeNull();
        }

        [Theory]
        [InlineData("Microsoft.Extensions.DependencyModel.dll")]
        [InlineData("Fake.Framework461.exe")]
        [InlineData("Fake.Core31.dll")]
        [Trait("Category", "Unit")]
        public void TryToLoadAssembly_ShouldLoadAssembly(string assemblyName)
        {
            // Arrange
            var assembly = Assembly.LoadFrom(assemblyName);

            // Act
            var actual = assemblyName.TryToLoadAssembly();

            // Assert
            actual.Should().BeSameAs(assembly);
        }

        [Theory]
        [AutoData]
        [Trait("Category", "Unit")]
        public void TryToLoadAssembly_UsingAssemblyName_ShouldReturnNull(string name)
        {
            // Arrange
            var assemblyName = new AssemblyName(name);

            // Act
            var actual = assemblyName.TryToLoadAssembly();

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void TryToLoadAssembly_UsingAssemblyName_ShouldLoadAssembly()
        {
            // Arrange
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName();

            // Act
            var actual = assemblyName.TryToLoadAssembly();

            // Assert
            actual.Should().BeSameAs(assembly);
        }

        [Theory]
        [ClassData(typeof(AssemblyTheoryData))]
        [Trait("Category", "Unit")]
        public void GetDependencies_UsingLocalDependencies_ShouldMatchLoaddingAssemblyCount(
            string assemblyName, IEnumerable<string> expectedAssemblyName)
        {
            // Arrange
            var assembly = Assembly.LoadFrom(assemblyName);

            // Act
            var actual = assembly.GetDependencies().Select(x => x.GetName().Name);

            // Assert
            actual.Should().BeEquivalentTo(expectedAssemblyName);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetDependencies_UsingCoreDependencies_ShouldBeEmpty()
        {
            // Arrange
            var assemblyName = "mscorlib";
            var assembly = Assembly.Load(assemblyName);
            var expected = new[] { assembly };

            // Act
            var actual = assembly.GetDependencies();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetPattern_WithExecutingAssembly_ShouldMatchProperPattern()
        {
            // Arrange
            var expected = "Tethos";
            var assemblyName = Assembly.GetExecutingAssembly().FullName;

            // Act
            var actual = assemblyName.GetPattern();

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [AutoData]
        [Trait("Category", "Unit")]
        public void GetPattern_WithSystemAssembly_ShouldThrowArgumentException(FakeAssembly assembly)
        {
            // Arrange
            Action action = () => assembly.FullName.GetPattern();

            // Act & Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("Tethos.dll", "Tethos.Tests.Common.dll")]
        [InlineData("AutoFixture.dll", "FluentAssertions.dll", "Moq.dll", "xunit.core.dll")]
        [InlineData("Castle.Core.dll", "Castle.Windsor.dll")]
        [Trait("Category", "Unit")]
        public void LoadAssemblies_ShouldLoad(params string[] assemblies)
        {
            // Arrange
            var files = assemblies.Select(x => x.GetFile());
            var expected = assemblies.Length;

            // Act
            var actual = files.LoadAssemblies();

            // Assert
            actual.Should().HaveCount(expected);
        }

        [Theory]
        [InlineData("Fake.Core30.exe", "Fake.Core31.exe")]
        [Trait("Category", "Unit")]
        public void LoadAssemblies_ShouldSkip(params string[] assemblies)
        {
            // Arrange
            var files = assemblies.Select(x => x.GetFile());

            // Act
            var actual = files.LoadAssemblies();

            // Assert
            actual.Should().BeEmpty();
        }

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
        [Trait("Category", "Unit")]
        public void FilterAssemblies_ShouldMatchCount(string pattern, int expected, params string[] assemblies)
        {
            // Arrange
            var extensions = new[] { ".dll", ".exe" };
            var files = assemblies.Select(x => x.GetFile());

            // Act
            var actual = files.FilterAssemblies(pattern, extensions);

            // Assert
            actual.Should().HaveCount(expected);
        }

        [Theory]
        [InlineData(0, "ref/Fake.Core31.dll", "foo/ref/Fake.Core31.dll")]
        [InlineData(0, "ref/Fake.Core31.ref.dll")]
        [InlineData(1, "Fake.Core31.dll")]
        [InlineData(2, "Fake.ref.Core31.dll", "Fake.ref.ref.ref")]
        [Trait("Category", "Unit")]
        public void ExcludeRefDirectory(int expected, params string[] assemblies)
        {
            // Arrange
            var files = assemblies.Select(x => x.GetFile());

            // Act
            var actual = files.ExcludeRefDirectory();

            // Assert
            actual.Should().HaveCount(expected);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ElseLoadReferencedAssemblies_Empty_ShouldReturnReferenceAssemblied()
        {
            // Arrange
            var assembly = Assembly.GetExecutingAssembly();
            var assemblies = Array.Empty<File>();
            var expected = assembly.GetReferencedAssemblies().Length;

            // Act
            var actual = assemblies.ElseLoadReferencedAssemblies(assembly);

            // Assert
            actual.Should().HaveCount(expected);
        }

        [Theory]
        [AutoData]
        [Trait("Category", "Unit")]
        internal void ElseLoadReferencedAssemblies_ShouldReturnOriginal(File[] files)
        {
            // Arrange
            var assembly = Assembly.GetExecutingAssembly();
            var expected = files.Length;

            // Act
            var actual = files.ElseLoadReferencedAssemblies(assembly);

            // Assert
            actual.Should().HaveCount(expected);
        }
    }
}
