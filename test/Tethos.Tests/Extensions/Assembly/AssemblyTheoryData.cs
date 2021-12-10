namespace Tethos.Tests.Extensions.Assembly
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Xunit;

    public class AssemblyTheoryData : TheoryData<string, IEnumerable<string>>
    {
        public AssemblyTheoryData()
        {
            this.Add("Fake.Core21.dll", GetMatchingAssemblies("Fake."));
            this.Add("Fake.Core22.dll", GetMatchingAssemblies("Fake."));
            this.Add("Fake.Core31.dll", GetMatchingAssemblies("Fake."));
            this.Add("Fake.Framework461.exe", GetMatchingAssemblies("Fake."));
            this.Add("Fake.Framework472.exe", GetMatchingAssemblies("Fake."));
            this.Add("Fake.Net50.dll", GetMatchingAssemblies("Fake."));
            this.Add("Fake.Net60.dll", GetMatchingAssemblies("Fake."));
            this.Add("Fake.Standard20.dll", GetMatchingAssemblies("Fake."));
            this.Add("Fake.Standard21.dll", GetMatchingAssemblies("Fake."));
            this.Add("Tethos.dll", GetMatchingAssemblies("Tethos."));
            this.Add("Tethos.Tests.dll", GetMatchingAssemblies("Tethos."));
            this.Add("Tethos.Tests.Common.dll", GetMatchingAssemblies("Tethos."));
        }

        private static IEnumerable<string> GetMatchingAssemblies(string pattern) => Directory
            .EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*.*", SearchOption.TopDirectoryOnly)
            .Select(file => Path.GetFileName(file))
            .Where(file => file.StartsWith(pattern))
            .Where(file => new[] { ".exe", ".dll" }.Contains(Path.GetExtension(file)))
            .Select(file => Path.GetFileNameWithoutExtension(file))
            .Distinct();
    }
}
