namespace Tethos.Tests.Extensions.Assembly;

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
    [InlineAutoData(typeof(IMockable))]
    [InlineAutoData(typeof(Assert))]
    [InlineAutoData(typeof(Xunit.Abstractions.ITest))]
    [InlineAutoData(typeof(Moq.IMock<>))]
    [InlineAutoData(typeof(FluentAssertions.Events.EventMetadata))]
    [InlineAutoData(typeof(AutoFixture.BehaviorRoot))]
    [InlineAutoData(typeof(Castle.Core.ParameterModel))]
    [InlineAutoData(typeof(Castle.Windsor.IWindsorContainer))]
    [InlineAutoData(typeof(System.Collections.IList))]
    [InlineAutoData(typeof(System.Collections.IEnumerable))]
    [InlineAutoData(typeof(Array))]
    [InlineAutoData(typeof(Enumerable))]
    [InlineAutoData(typeof(Type))]
    [InlineAutoData(typeof(BaseAutoResolver))]
    [InlineAutoData(typeof(TimeoutException))]
    [InlineAutoData(typeof(Guid))]
    [InlineAutoData(typeof(Task<>))]
    [InlineAutoData(typeof(Task<int>))]
    [InlineAutoData(typeof(int))]
    [Trait("Type", "Unit")]
    public void GetAssemblies_ShouldLoadAssembly(Type type, AutoMockingConfiguration configuration)
    {
        // Arrange
        var expected = type.Assembly;

        // Act
        var actual = type.GetAssemblies(configuration);

        // Assert
        actual.Should().Contain(expected);
    }

    [Theory(Skip = "Use different assembly")]
    [AutoData]
    [Trait("Type", "Unit")]
    public void GetAssemblies_UsingMicrosoftCorLib_ShouldLoad(AutoMockingConfiguration configuration)
    {
        // Arrange
        var assemblyName = "mscorlib";
        var assembly = Assembly.Load(assemblyName);
        var expected = new[] { assembly };
        var type = assembly.DefinedTypes.First();

        // Act
        var actual = type.GetAssemblies(configuration);

        // Assert
        actual.Should().Contain(expected);
    }

    [Theory]
    [ClassData(typeof(AssemblyTheoryData))]
    [Trait("Type", "Unit")]
    public void GetAssemblies_ShouldLoad(
        string assemblyName,
        IEnumerable<string> expected,
        AutoMockingConfiguration configuration)
    {
        // Arrange
        var assembly = Assembly.LoadFrom(assemblyName);
        var type = assembly.DefinedTypes.First();

        // Act
        var actual = type.GetAssemblies(configuration).Select(dependency => dependency.GetName().Name);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    [Trait("Type", "Unit")]
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
    [Trait("Type", "Unit")]
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
