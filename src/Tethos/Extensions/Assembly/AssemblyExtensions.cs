namespace Tethos.Extensions.Assembly;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

internal static class AssemblyExtensions
{
    /// <summary>
    /// Retrives related assemblies.
    /// </summary>
    internal static Assembly[] GetAssemblies(
        this Type type,
        AutoMockingConfiguration configuration)
    {
        var rootAssembly = Assembly.GetAssembly(type);
        var appDomain = AppDomain.CurrentDomain.BaseDirectory.GetFiles();

        var assemblies = configuration.LoadingMethod switch
        {
            AutoMockingLoadingTypes.All => appDomain.FilterAssemblies(new[] { ".dll", ".exe" }, rootAssembly),
            AutoMockingLoadingTypes.PatternFromSourceAssembly => appDomain.FilterAssemblies(rootAssembly.FullName.GetPattern(), new[] { ".dll", ".exe" }, rootAssembly),
            AutoMockingLoadingTypes.ReferencedAssemblies => appDomain.ElseLoadReferencedAssemblies(rootAssembly),
            _ => throw new NotImplementedException(),
        };

        return assemblies
            .ExcludeRefDirectory()
            .LoadAssemblies(rootAssembly)
            .ToArray();
    }

    internal static IEnumerable<File> ElseLoadReferencedAssemblies(
        this IEnumerable<File> assemblies,
        Assembly rootAssembly) => assemblies.Any() switch
        {
            true => assemblies,
            false => rootAssembly
                        .GetReferencedAssemblies()
                        .Select(assembly => assembly.TryToLoadAssembly())
                        .OfType<Assembly>()
                        .Select(assembly => new Uri(assembly.CodeBase).AbsolutePath)
                        .Select(filePath => filePath.GetFile()),
        };
}
