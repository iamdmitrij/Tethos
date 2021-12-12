namespace Tethos.NSubstitute.Tests.Benchmarks
{
    using BenchmarkDotNet.Attributes;

    [BenchmarkCategory("NSubstitute")]
    [ShortRunJob]
    public class FactoryBenchmark
    {
        [Benchmark(Description = "NSubstitute")]
        public void MakeFactory() => AutoMockingContainerFactory.Create();
    }
}