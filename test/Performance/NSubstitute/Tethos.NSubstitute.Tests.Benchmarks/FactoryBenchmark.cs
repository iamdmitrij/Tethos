namespace Tethos.NSubstitute.Tests.Benchmarks
{
    using BenchmarkDotNet.Attributes;

    [BenchmarkCategory("NSubstitute")]
    [ShortRunJob]
    public class FactoryBenchmark
    {
        [Benchmark]
        public void MakeFactory() => AutoMockingContainerFactory.Create();
    }
}