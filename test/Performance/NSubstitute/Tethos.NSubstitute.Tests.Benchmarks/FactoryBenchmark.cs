namespace Tethos.NSubstitute.Tests.Benchmarks
{
    using BenchmarkDotNet.Attributes;

    [BenchmarkCategory("NSubstitute")]
    [ShortRunJob]
    public class FactoryBenchmark
    {
        [Benchmark(Description = "NSubstitute")]
        public static void MakeFactory() => AutoMockingContainerFactory.Create();
    }
}