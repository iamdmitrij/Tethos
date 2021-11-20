using System.IO;

namespace Tethos
{
    internal static class FileExtensions
    {
        internal static File GetFile(this string filePath) =>
            new File
            {
                Path = filePath,
                Name = Path.GetFileName(filePath),
                Extension = Path.GetExtension(filePath).ToLowerInvariant(),
                Directory = Path.GetDirectoryName(filePath),
            };
    }
}
