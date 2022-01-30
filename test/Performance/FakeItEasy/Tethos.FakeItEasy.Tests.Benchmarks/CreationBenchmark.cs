namespace Tethos.FakeItEasy.Tests.Benchmarks
{
    using System.Diagnostics.CodeAnalysis;
    using BenchmarkDotNet.Attributes;

    [ShortRunJob]
    public class CreationBenchmark
    {
        [Benchmark(Description = "FakeItEasy.MakeFactory")]
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public void MakeFactory() => AutoMocking.Create();
    }
}
