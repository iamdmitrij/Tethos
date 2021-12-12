namespace Tethos.FakeItEasy.Tests.Benchmarks
{
    using BenchmarkDotNet.Attributes;

    [ShortRunJob]
    public class FactoryBenchmark
    {
        [Benchmark(Description = "FakeItEasy")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public void MakeFactory() => AutoMockingContainerFactory.Create();
    }
}
