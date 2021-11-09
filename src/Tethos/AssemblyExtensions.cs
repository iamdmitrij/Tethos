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

        internal static IEnumerable<File> GetAssemblyFiles(this string directory) =>
            Directory
                .EnumerateFiles(directory, "*.*", SearchOption.AllDirectories)
                .Select(filePath => filePath.GetFile());

        internal static IEnumerable<File> FilterAssemblies(this IEnumerable<File> assemblies, string searchPattern, string[] allowedFileExtensions, params Assembly[] rootAssemblies) =>
            assemblies
                .Where(file => allowedFileExtensions.Contains(file.Extension))
                .Where(file => file.Name.Contains(searchPattern))
                .Where(file => !rootAssemblies.Select(assembly => Path.GetFileName(assembly.Location)).Contains(file.Name));

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

        internal static IEnumerable<File> ExcludeRefDirectory(this IEnumerable<File> assemblies) =>
            assemblies
                .Where(file => !file.Directory.EndsWith("ref"));

        internal static IEnumerable<File> ElseLoadReferencedAssemblies(this IEnumerable<File> assemblies, Assembly rootAssembly) =>
            assemblies.Any()
                ? assemblies
                : rootAssembly
                    .GetReferencedAssemblies()
                    .Select(TryToLoadAssembly)
                    .OfType<Assembly>()
                    .Select(assembly => new Uri(assembly.CodeBase).AbsolutePath)
                    .Select(filePath => filePath.GetFile());

        internal static IEnumerable<Assembly> LoadAssemblies(this IEnumerable<File> assemblies, params Assembly[] extraAssemblies) =>
            assemblies.Select(file => file.Name)
                .Select(TryToLoadAssembly)
                .OfType<Assembly>()
                .Union(extraAssemblies);

        internal static Assembly TryToLoadAssembly(this AssemblyName assemblyName)
        {
            var func = () => Assembly.Load(assemblyName);
            return func.SwallowExceptions(typeof(BadImageFormatException), typeof(FileNotFoundException));
        }

        internal static Assembly TryToLoadAssembly(this string assemblyPath)
        {
            var func = () => Assembly.LoadFrom(assemblyPath);
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
