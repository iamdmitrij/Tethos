namespace Tethos.Tests.Extensions.Assembly
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AutoFixture.Xunit2;
    using FluentAssertions;
    using Tethos.Extensions.Assembly;
    using Xunit;

    public class AssemblyExtensionsTests : BaseAutoMockingTest<AutoMockingContainer>
    {
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
