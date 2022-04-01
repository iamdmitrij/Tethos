namespace Tethos.Benchmarks
{
    using System.Diagnostics.CodeAnalysis;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Mathematics;
    using BenchmarkDotNet.Order;

    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn(NumeralSystem.Arabic)]
    [MemoryDiagnoser]
    [ShortRunJob]
    public class CreationBenchmark
    {
        [Benchmark(Description = "FakeItEasy.CreateContainer")]
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public void CreateFakeItEasy() => FakeItEasy.AutoMocking.Create();

        [Benchmark(Description = "Moq.CreateContainer")]
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public void CreateMoq() => Moq.AutoMocking.Create();

        [Benchmark(Description = "NSubstitute.CreateContainer")]
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public void CreateNSubstitute() => NSubstitute.AutoMocking.Create();
    }
}
