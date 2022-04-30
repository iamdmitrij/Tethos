namespace Tethos.Extensions.Assembly;

using System;

internal static class AssemblyPatternExtensions
{
    internal static string GetPattern(
        this string assemblyName) => assemblyName?.IndexOfAny(new[] { '.', ',' }) switch
        {
            var index when !index.HasValue || index < 0 => throw new ArgumentException("Could not determine pattern " +
                $@"for assembly named ""{assemblyName}"". Please use a different method for obtaining assemblies."),
            var index => assemblyName.Substring(0, index.Value),
        };
}
