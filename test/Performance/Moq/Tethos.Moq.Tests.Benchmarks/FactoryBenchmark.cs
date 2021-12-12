namespace Tethos.Moq.Tests.Benchmarks
{
    using BenchmarkDotNet.Attributes;

    [BenchmarkCategory("Moq")]
    [ShortRunJob]
    public class FactoryBenchmark
    {
        [Benchmark(Description = "Moq")]
        public void MakeFactory() => AutoMockingContainerFactory.Create();
    }
}
