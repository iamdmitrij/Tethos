namespace Tethos.Tests.Extensions.Assembly
{
    using System.Linq;
    using System.Reflection;
    using AutoFixture.Xunit2;
    using FluentAssertions;
    using Tethos.Extensions.Assembly;
    using Xunit;

    public class AssemblyLoadingExtensionsTests : BaseAutoMockingTest<AutoMockingContainer>
    {
        [Theory]
        [InlineData("Tethos.dll", "Tethos.Tests.Common.dll")]
        [InlineData("AutoFixture.dll", "FluentAssertions.dll", "Moq.dll", "xunit.core.dll")]
        [InlineData("Castle.Core.dll", "Castle.Windsor.dll")]
        [Trait("Category", "Unit")]
        public void LoadAssemblies_ShouldLoad(params string[] assemblies)
        {
            // Arrange
            var files = assemblies.Select(assembly => assembly.GetFile());
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
            var files = assemblies.Select(assembly => assembly.GetFile());

            // Act
            var actual = files.LoadAssemblies();

            // Assert
            actual.Should().BeEmpty();
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
        public void TryToLoadAssembly_UsingAssemblyName_ShouldLoad()
        {
            // Arrange
            var expected = Assembly.GetExecutingAssembly();
            var assemblyName = expected.GetName();

            // Act
            var actual = assemblyName.TryToLoadAssembly();

            // Assert
            actual.Should().BeSameAs(expected);
        }

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
        public void TryToLoadAssembly_ShouldLoad(string assemblyName)
        {
            // Arrange
            var expected = Assembly.LoadFrom(assemblyName);

            // Act
            var actual = assemblyName.TryToLoadAssembly();

            // Assert
            actual.Should().BeSameAs(expected);
        }
    }
}
