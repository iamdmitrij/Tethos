namespace Tethos.Tests.Extensions.Assembly
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using AutoFixture;
    using Xunit;

    public class AssemblyTheoryData : TheoryData<string, IEnumerable<string>, AutoMockingConfiguration>
    {
        public AssemblyTheoryData()
        {
            var configuration = new Fixture().Create<AutoMockingConfiguration>();

            this.Add("Fake.Core21.dll", GetMatchingAssemblies("Fake."), configuration);
            this.Add("Fake.Core22.dll", GetMatchingAssemblies("Fake."), configuration);
            this.Add("Fake.Core31.dll", GetMatchingAssemblies("Fake."), configuration);
            this.Add("Fake.Framework461.exe", GetMatchingAssemblies("Fake."), configuration);
            this.Add("Fake.Framework472.exe", GetMatchingAssemblies("Fake."), configuration);
            this.Add("Fake.Net50.dll", GetMatchingAssemblies("Fake."), configuration);
            this.Add("Fake.Net60.dll", GetMatchingAssemblies("Fake."), configuration);
            this.Add("Fake.Standard20.dll", GetMatchingAssemblies("Fake."), configuration);
            this.Add("Fake.Standard21.dll", GetMatchingAssemblies("Fake."), configuration);
            this.Add("Tethos.dll", GetMatchingAssemblies("Tethos."), configuration);
            this.Add("Tethos.Tests.dll", GetMatchingAssemblies("Tethos."), configuration);
            this.Add("Tethos.Tests.Common.dll", GetMatchingAssemblies("Tethos."), configuration);
        }

        private static IEnumerable<string> GetMatchingAssemblies(string pattern) => Directory
            .EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*.*", SearchOption.TopDirectoryOnly)
            .Select(file => Path.GetFileName(file))
            .Where(file => new[] { ".exe", ".dll" }.Contains(Path.GetExtension(file)))
            .Select(file => Path.GetFileNameWithoutExtension(file))
            .Distinct();
    }
}
