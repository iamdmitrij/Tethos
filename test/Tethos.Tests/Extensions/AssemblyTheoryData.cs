namespace Tethos.Tests.Extensions
{
    using System.Collections.Generic;
    using Xunit;

    public class AssemblyTheoryData : TheoryData<string, IEnumerable<string>>
    {
        public AssemblyTheoryData()
        {
            var fakeCore21 = new[]
            {
                "Fake.Core21",
                "Fake.Core22",
                "Fake.Core30",
                "Fake.Core31",
                "Fake.Net50",
                "Fake.Net60",
                "Fake.Framework461",
                "Fake.Framework472",
                "Fake.Standard20",
                "Fake.Standard21",
            };

            this.Add("Fake.Core21.dll", fakeCore21);

            var fakeCore31 = new[]
            {
                "Fake.Core21",
                "Fake.Core22",
                "Fake.Core30",
                "Fake.Core31",
                "Fake.Net50",
                "Fake.Net60",
                "Fake.Framework461",
                "Fake.Framework472",
                "Fake.Standard20",
                "Fake.Standard21",
            };

            this.Add("Fake.Core31.dll", fakeCore31);

            var fakeCore50 = new[]
            {
                "Fake.Core21",
                "Fake.Core22",
                "Fake.Core30",
                "Fake.Core31",
                "Fake.Net50",
                "Fake.Net60",
                "Fake.Framework461",
                "Fake.Framework472",
                "Fake.Standard20",
                "Fake.Standard21",
            };

            this.Add("Fake.Net50.dll", fakeCore50);

            var tethos = new[]
            {
                "Tethos",
                "Tethos.Tests",
                "Tethos.Tests.Common",
            };

            this.Add("Tethos.dll", tethos);

            var tethosTests = new[]
            {
                "Tethos",
                "Tethos.Tests",
                "Tethos.Tests.Common",
            };

            this.Add("Tethos.Tests.dll", tethosTests);

            var tethosTestsCommon = new[]
            {
                "Tethos",
                "Tethos.Tests",
                "Tethos.Tests.Common",
            };
            this.Add("Tethos.Tests.Common.dll", tethosTestsCommon);
        }
    }
}
