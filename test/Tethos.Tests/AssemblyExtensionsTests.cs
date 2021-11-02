namespace Tethos.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AutoFixture.Xunit2;
    using FluentAssertions;
    using Xunit;

    public class AssemblyExtensionsTests : BaseAutoMockingTest<AutoMockingContainer>
    {
        [Fact]
        public void AllowedExtensions_ShouldBeOfTypeHashSet()
        {
            // Act
            var actual = AssemblyExtensions.FileExtensions;

            // Assert
            actual.Should().NotBeEmpty().And.BeOfType<HashSet<string>>();
        }

        [Fact]
        public void TryToLoadAssembly_WithCorruptAssembly_ShouldReturnNull()
        {
            // Arrange
            var corruptAssembly = "Fake.Core30.exe";

            // Act
            var actual = AssemblyExtensions.TryToLoadAssembly(corruptAssembly);

            // Assert
            actual.Should().BeNull();
        }

        [Theory]
        [InlineData("Microsoft.Extensions.DependencyModel.dll")]
        [InlineData("Fake.Framework461.exe")]
        [InlineData("Fake.Core31.dll")]
        public void TryToLoadAssembly_ShouldLoadAssembly(string assemblyName)
        {
            // Act
            var actual = AssemblyExtensions.TryToLoadAssembly(assemblyName);

            // Assert
            actual.Should().NotBeNull();
        }

        [Theory]
        [ClassData(typeof(AssemblyTheoryData))]
        public void GetDependencies_UsingLocalDependencies_ShouldMatchLoaddingAssemblyCount(
            string assemblyName, IEnumerable<string> expectedAssemblyName
        )
        {
            // Arrange
            var assembly = Assembly.LoadFrom(assemblyName);

            // Act
            var actual = assembly.GetDependencies().Select(x => x.GetName().Name);

            // Assert
            actual.Should().BeEquivalentTo(expectedAssemblyName);
        }

        [Fact]
        public void GetDependencies_UsingCoreDependencies_ShouldBeEmpty()
        {
            // Arrange
            var assemblyName = "mscorlib";
            var assembly = Assembly.Load(assemblyName);

            // Act
            var actual = assembly.GetDependencies();

            // Assert
            actual.Should().BeEmpty();
        }

        [Fact]
        public void GetPattern_WithExecutingAssembly_ShouldMatchProperPattern()
        {
            // Arrange
            var expected = "Tethos";
            var assembly = Assembly.GetExecutingAssembly();

            // Act
            var actual = assembly.GetPattern();

            // Assert
            actual.Should().Be(expected);
        }

        [Theory, AutoData]
        public void GetPattern_WithSystemAssembly_ShouldThrowArgumentException(FakeAssembly assembly)
        {
            // Arrange
            Action action = () => assembly.GetPattern();

            // Act & Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("Tethos.dll", "Tethos.Tests.Common.dll")]
        [InlineData("AutoFixture.dll", "FluentAssertions.dll", "Moq.dll", "xunit.core.dll")]
        [InlineData("Castle.Core.dll", "Castle.Windsor.dll")]
        public void LoadAssemblies_ShouldLoad(params string[] assemblies)
        {
            // Arrange
            var expected = assemblies.Length;

            // Act
            var actual = assemblies.LoadAssemblies();

            // Assert
            actual.Should().HaveCount(expected);
        }

        [Theory]
        [InlineData("Fake.Core30.exe", "Fake.Core31.exe")]
        public void LoadAssemblies_ShouldSkip(params string[] assemblies)
        {
            // Act
            var actual = assemblies.LoadAssemblies();

            // Assert
            actual.Should().BeEmpty();
        }

        [Theory]
        [InlineData("", 0, "")]
        [InlineData("Fake", 0, "")]
        [InlineData("Fake", 2, "Fake.Core31.dll", "Fake.Core31.pdb", "Fake.Core31.exe")]
        [InlineData("Fake", 0, "Fake.Standard20.deps.json")]
        [InlineData("Fake", 1, "Fake.Standard20.dll")]
        [InlineData("Tethos", 0, "ref/Tethos.Tests.dll")]
        [InlineData("Tethos", 1, "ref/Tethos.Tests.dll", "Tethos.Tests.Common.dll")]
        [InlineData("Tethos", 0, "Fake.Standard20.dll")]
        [InlineData("Tethos", 0, "Fake.Standard20.dll", "ref/Tethos.Tests.dll")]
        [InlineData("xunit", 1, "xunit.abstractions.dll")]
        public void FilterAssemblies_ShouldMatchCount(string pattern, int expected, params string[] assemblies)
        {
            // Act
            var actual = assemblies.FilterAssemblies(pattern);

            // Assert
            actual.Should().HaveCount(expected);
        }
    }
}
