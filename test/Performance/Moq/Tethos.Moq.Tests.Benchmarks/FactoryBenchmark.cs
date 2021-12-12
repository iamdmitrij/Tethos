namespace Tethos.Moq.Tests.Benchmarks
{
    using BenchmarkDotNet.Attributes;

    [ShortRunJob]
    public class FactoryBenchmark
    {
        [Benchmark]
        public void MakeFactory() => AutoMockingContainerFactory.Create();
    }
}