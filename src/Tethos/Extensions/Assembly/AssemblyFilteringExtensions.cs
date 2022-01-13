namespace Tethos.Extensions.Assembly
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    internal static class AssemblyFilteringExtensions
    {
        internal static IEnumerable<File> FilterAssemblies(
            this IEnumerable<File> assemblies,
            string searchPattern,
            string[] allowedFileExtensions,
            params Assembly[] rootAssemblies) => assemblies
                .Where(file => allowedFileExtensions.Contains(file.Extension))
                .Where(file => file.Name.Contains(searchPattern))
                .Where(file => !rootAssemblies 
                    .Select(assembly => Path.GetFileName(assembly.Location))
                    .Any(fileName => fileName == file.Name));

        internal static IEnumerable<File> ExcludeRefDirectory(
            this IEnumerable<File> assemblies) =>
            assemblies
                .Where(file => !file.Directory.EndsWith("ref"));
    }
}
