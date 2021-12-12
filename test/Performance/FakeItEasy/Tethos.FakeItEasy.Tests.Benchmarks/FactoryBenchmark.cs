namespace Tethos.FakeItEasy.Tests.Benchmarks
{
    using BenchmarkDotNet.Attributes;

    [BenchmarkCategory("FakeItEasy")]
    [ShortRunJob]
    public class FactoryBenchmark
    {
        [Benchmark(Description = "FakeItEasy")]
        public void MakeFactory() => AutoMockingContainerFactory.Create();
    }
}
