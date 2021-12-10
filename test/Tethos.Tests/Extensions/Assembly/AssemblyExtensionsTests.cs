namespace Tethos.Tests.Extensions.Assembly
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using FluentAssertions;
    using Tethos.Extensions.Assembly;
    using Tethos.Tests.Common;
    using Xunit;

    public class AssemblyExtensionsTests : BaseAutoMockingTest<AutoMockingContainer>
    {
        [Theory]
        [InlineData(typeof(IMockable))]
        [InlineData(typeof(Assert))]
        [InlineData(typeof(Xunit.Abstractions.ITest))]
        [InlineData(typeof(Moq.IMock<>))]
        [InlineData(typeof(FluentAssertions.Events.EventMetadata))]
        [InlineData(typeof(AutoFixture.BehaviorRoot))]
        [InlineData(typeof(Castle.Core.ParameterModel))]
        [InlineData(typeof(Castle.Windsor.IWindsorContainer))]
        [InlineData(typeof(System.Collections.IList))]
        [InlineData(typeof(System.Collections.IEnumerable))]
        [InlineData(typeof(Array))]
        [InlineData(typeof(Enumerable))]
        [InlineData(typeof(Type))]
        [InlineData(typeof(BaseAutoResolver))]
        [InlineData(typeof(TimeoutException))]
        [InlineData(typeof(Guid))]
        [InlineData(typeof(Task<>))]
        [InlineData(typeof(Task<int>))]
        [InlineData(typeof(int))]
        [Trait("Category", "Unit")]
        public void GetRelatedAssemblies_ShouldLoad(Type type)
        {
            // Arrange
            var expected = type.Assembly;

            // Act
            var actual = type.GetRelatedAssemblies();

            // Assert
            actual.Should().Contain(expected);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetDependencies_UsingMicrosoftCorLib_ShouldLoad()
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

        [Theory]
        [ClassData(typeof(AssemblyTheoryData))]
        [Trait("Category", "Unit")]
        public void GetDependencies_ShouldLoad(string assemblyName, IEnumerable<string> expected)
        {
            // Arrange
            var assembly = Assembly.LoadFrom(assemblyName);

            // Act
            var actual = assembly.GetDependencies().Select(dependency => dependency.GetName().Name);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ElseLoadReferencedAssemblies_Empty_ShouldHaveReferencedAssembliesCount()
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
        internal void ElseLoadReferencedAssemblies_ShouldMatchFileCount(File[] files)
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
