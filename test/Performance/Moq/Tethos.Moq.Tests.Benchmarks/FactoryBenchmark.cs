namespace Tethos.Moq.Tests.Benchmarks
{
    using BenchmarkDotNet.Attributes;

    [BenchmarkCategory("Moq")]
    [ShortRunJob]
    public class FactoryBenchmark
    {
        [Benchmark]
        public void MakeFactory() => AutoMockingContainerFactory.Create();
    }
}