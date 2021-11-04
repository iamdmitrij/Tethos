using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Tethos
{
    /// <summary>
    /// Extension utilities used by <see cref="BaseAutoMockingTest{T}"/>.
    /// </summary>
    internal static class AssemblyExtensions
    {
        internal static Assembly[] GetDependencies(this Assembly rootAssembly) =>
            AppDomain.CurrentDomain.BaseDirectory.GetAssemblyFiles()
                .FilterAssemblies(rootAssembly.GetPattern(), new[] { ".dll", ".exe" }, rootAssembly)
                .ExcludeRefDirectory()
                .ElseLoadReferencedAssemblies(rootAssembly)
                .LoadAssemblies(rootAssembly)
                .ToArray();

        internal static IEnumerable<string> GetAssemblyFiles(this string directory) =>
            Directory
                .EnumerateFiles(directory, "*.*", SearchOption.AllDirectories);

        internal static IEnumerable<string> FilterAssemblies(this IEnumerable<string> assemblies, string searchPattern, string[] fileExtensions, params Assembly[] rootAssemblies) =>
            assemblies
                .Where(filePath => fileExtensions.Contains(Path.GetExtension(filePath).ToLowerInvariant()))
                .Where(fileName => Path.GetFileName(fileName).Contains(searchPattern))
                .Where(fileName => !rootAssemblies.Select(assembly => Path.GetFileName(assembly.Location)).Contains(Path.GetFileName(fileName)));

        internal static string GetPattern(this Assembly rootAssembly)
        {
            var patternSeparators = new[] { '.', ',' };
            var name = rootAssembly.FullName;
            var index = name.IndexOfAny(patternSeparators);

            if (index < 0)
            {
                throw new ArgumentException($"Could not determine application name for assembly {name}. " +
                    "Please use a different method for obtaining assemblies.");
            }

            var pattern = name.Substring(0, index);

            return pattern;
        }

        internal static IEnumerable<string> ExcludeRefDirectory(this IEnumerable<string> assemblies) =>
            assemblies
                .Where(filePath => !Path.GetDirectoryName(filePath).EndsWith("ref"));

        internal static IEnumerable<string> ElseLoadReferencedAssemblies(this IEnumerable<string> assemblies, Assembly rootAssembly) =>
            assemblies.Any() ? assemblies : rootAssembly.GetReferencedAssemblies().Select(TryToLoadAssembly).OfType<Assembly>().Select(GetPath);

        internal static string GetPath(this Assembly assembly) =>
            new Uri(assembly.CodeBase).AbsolutePath;

        internal static IEnumerable<Assembly> LoadAssemblies(this IEnumerable<string> assemblies, params Assembly[] extras) =>
            assemblies.Select(Path.GetFileName)
                .Select(TryToLoadAssembly)
                .OfType<Assembly>()
                .Union(extras);

        internal static Assembly TryToLoadAssembly(this AssemblyName assemblyName)
        {
            // TODO: Explicit Func type won't necessary in C# 10
            Func<Assembly> func = () => Assembly.Load(assemblyName);
            return func.SwallowExceptions(typeof(BadImageFormatException), typeof(FileNotFoundException));
        }

        internal static Assembly TryToLoadAssembly(this string assemblyPath)
        {
            // TODO: Explicit Func type won't necessary in C# 10
            Func<Assembly> func = () => Assembly.LoadFrom(assemblyPath);
            return func.SwallowExceptions(typeof(BadImageFormatException), typeof(FileNotFoundException));
        }

        internal static Assembly SwallowExceptions(this Func<Assembly> func, params Type[] types)
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception ex) when (types.Contains(ex.GetType()))
            {
                return null;
            }
        }
    }
}
