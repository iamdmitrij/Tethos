namespace Tethos.Extensions.Assembly
{
    using System;

    internal static class AssemblyPatternExtensions
    {
        internal static string GetPattern(
            this string assemblyName) => assemblyName.IndexOfAny(new[] { '.', ',' }) switch
            {
                var index when index < 0 => throw new ArgumentException("Could not determine application name " +
                    $"for assembly {assemblyName}. Please use a different method for obtaining assemblies."),
                var index => assemblyName.Substring(0, index),
            };
    }
}
