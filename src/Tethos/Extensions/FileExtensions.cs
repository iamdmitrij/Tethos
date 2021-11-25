namespace Tethos.Extensions
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal static class FileExtensions
    {
        internal static IEnumerable<File> GetAssemblyFiles(
            this string directory) => Directory
                .EnumerateFiles(directory, "*.*", SearchOption.AllDirectories)
                .Select(GetFile);

        internal static File GetFile(this string filePath) =>
            new()
            {
                Path = filePath,
                Name = Path.GetFileName(filePath),
                Extension = Path.GetExtension(filePath).ToLowerInvariant(),
                Directory = Path.GetDirectoryName(filePath),
            };
    }
}
