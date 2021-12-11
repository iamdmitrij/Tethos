namespace Tethos.Extensions.Assembly
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal static class AssemblyExtensions
    {
        internal static Assembly[] GetRelatedAssemblies(this Type type) =>
            Assembly.GetAssembly(type).GetDependencies();

        internal static Assembly[] GetDependencies(
            this Assembly rootAssembly) => AppDomain.CurrentDomain.BaseDirectory.GetAssemblyFiles()
                .FilterAssemblies(rootAssembly.FullName.GetPattern(), new[] { ".dll", ".exe" }, rootAssembly)
                .ExcludeRefDirectory()
                .ElseLoadReferencedAssemblies(rootAssembly)
                .LoadAssemblies(rootAssembly)
                .ToArray();

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
}
