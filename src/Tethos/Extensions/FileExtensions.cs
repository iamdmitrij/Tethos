namespace Tethos.Extensions
{
    using System.IO;
    using File = Tethos.File;

    internal static class FileExtensions
    {
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
