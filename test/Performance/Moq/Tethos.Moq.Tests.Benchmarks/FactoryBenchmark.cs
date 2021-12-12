namespace Tethos.Moq.Tests.Benchmarks
{
    using BenchmarkDotNet.Attributes;

    [ShortRunJob]
    public class FactoryBenchmark
    {
        [Benchmark(Description = "Moq.MakeFactory")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public void MakeFactory() => AutoMockingContainerFactory.Create();
    }
}
