namespace Tethos.Extensions.Assembly
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    internal static class AssemblyLoadingExtensions
    {
        internal static IEnumerable<Assembly> LoadAssemblies(
            this IEnumerable<File> assemblies,
            params Assembly[] extraAssemblies) => assemblies
                .Select(file => file.Name)
                .Select(TryToLoadAssembly)
                .OfType<Assembly>()
                .Union(extraAssemblies);

        internal static Assembly TryToLoadAssembly(
            this AssemblyName assemblyName)
        {
            var func = () => Assembly.Load(assemblyName);
            return func.SwallowExceptions(typeof(BadImageFormatException), typeof(FileNotFoundException));
        }

        internal static Assembly TryToLoadAssembly(
            this string assemblyPath)
        {
            var func = () => Assembly.LoadFrom(assemblyPath);
            return func.SwallowExceptions(typeof(BadImageFormatException), typeof(FileNotFoundException));
        }
    }
}
