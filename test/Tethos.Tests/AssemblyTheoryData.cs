namespace Tethos.Tests
{
    using System.Collections.Generic;
    using Xunit;

    public class AssemblyTheoryData : TheoryData<string, IEnumerable<string>>
    {
        public AssemblyTheoryData()
        {
            Add("Fake.Core21.dll",
                new[]
                {
                    "Fake.Core21",
                    "Fake.Core22",
                    "Fake.Core30",
                    "Fake.Core31",
                    "Fake.Net50",
                    "Fake.Framework461",
                    "Fake.Framework472",
                    "Fake.Standard20",
                    "Fake.Standard21",
                }
            );

            Add("Fake.Core31.dll",
                new[]
                {
                    "Fake.Core21",
                    "Fake.Core22",
                    "Fake.Core30",
                    "Fake.Core31",
                    "Fake.Net50",
                    "Fake.Framework461",
                    "Fake.Framework472",
                    "Fake.Standard20",
                    "Fake.Standard21",
                }
            );

            Add("Fake.Net50.dll",
                new[]
                {
                    "Fake.Core21",
                    "Fake.Core22",
                    "Fake.Core30",
                    "Fake.Core31",
                    "Fake.Net50",
                    "Fake.Framework461",
                    "Fake.Framework472",
                    "Fake.Standard20",
                    "Fake.Standard21",
                }
            );

            Add("Tethos.dll",
                new[]
                {
                    "Tethos",
                    "Tethos.Tests",
                    "Tethos.Tests.Common",
                }
            );

            Add("Tethos.Tests.dll",
                new[]
                {
                    "Tethos",
                    "Tethos.Tests",
                    "Tethos.Tests.Common",
                }
            );

            Add("Tethos.Tests.Common.dll",
                new[]
                {
                    "Tethos",
                    "Tethos.Tests",
                    "Tethos.Tests.Common",
                }
            );
        }
    }
}
