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
        /// <summary>
        /// A collection of allowed file extensions for container assemblies.
        /// </summary>
        internal static HashSet<string> FileExtensions { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".dll", ".exe"
        };

        /// <summary>
        /// Gets assembly search criteria.
        /// </summary>
        /// <param name="rootAssembly">Reference assembly for pattern extraction.</param>
        /// <returns>Pattern search criteria.</returns>
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

        /// <summary>
        /// Get all dependencies from entry assembly and file search pattern.
        /// </summary>
        /// <param name="rootAssembly">Reference assembly for pattern extraction.</param>
        /// <returns>A collection of loaded assemblies.</returns>
        internal static Assembly[] GetDependencies(this Assembly rootAssembly)
        {
            var pattern = rootAssembly.GetPattern();
            var directory = AppDomain.CurrentDomain.BaseDirectory;

            return Directory
                .EnumerateFiles(directory, "*.*", SearchOption.AllDirectories)
                .FilterAssemblies(pattern)
                .ElseLoadReferencedAssemblies(rootAssembly)
                .FilterRef()
                .LoadAssemblies();
        }

        internal static IEnumerable<string> FilterRef(this IEnumerable<string> assemblies) =>
            assemblies
                .Where(filePath => !Path.GetDirectoryName(filePath).EndsWith("ref"));

        internal static string GetPath(this Assembly assembly) =>
            new Uri(assembly.CodeBase).AbsolutePath;

        internal static IEnumerable<string> ElseLoadReferencedAssemblies(this IEnumerable<string> assemblies, Assembly rootAssembly) =>
            assemblies.Any() ? assemblies : rootAssembly.GetReferencedAssemblies().Select(TryToLoadAssembly).OfType<Assembly>().Select(GetPath);

        internal static Assembly[] LoadAssemblies(this IEnumerable<string> assemblies) =>
            assemblies.Select(Path.GetFileName)
                .Select(TryToLoadAssembly)
                .OfType<Assembly>()
                .ToArray();

        internal static IEnumerable<string> FilterAssemblies(this IEnumerable<string> assemblies, string searchPattern) =>
            assemblies
                .Where(filePath => FileExtensions.Contains(Path.GetExtension(filePath)))
                .Where(fileName => Path.GetFileName(fileName).Contains(searchPattern));

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

        internal static Assembly TryToLoadAssembly(this AssemblyName assemblyName)
        {
            Func<Assembly> func = () => Assembly.Load(assemblyName);
            return func.SwallowExceptions(typeof(BadImageFormatException), typeof(FileNotFoundException));
        }

        /// <summary>
        /// Silently loads assembly by its name.
        /// If any failure occurs in the assembly format, it will return null value.
        /// </summary>
        internal static Assembly TryToLoadAssembly(this string assemblyPath)
        {
            Func<Assembly> func = () => Assembly.LoadFrom(assemblyPath);
            return func.SwallowExceptions(typeof(BadImageFormatException), typeof(FileNotFoundException));
        }
    }
}
