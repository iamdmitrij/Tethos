namespace Tethos.Benchmarks.NonPublicTypes
{
    using System.Diagnostics.CodeAnalysis;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Mathematics;
    using BenchmarkDotNet.Order;

    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn(NumeralSystem.Arabic)]
    [MemoryDiagnoser]
    public class NonPublicTypesCreationBenchmark
    {
        [Benchmark(Description = "FakeItEasy.NonPublicTypes.CreateContainer")]
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public FakeItEasy.AutoMockingTest CreateFakeItEasy() => new FakeItEasyAutoMockingTest();

        [Benchmark(Description = "Moq.NonPublicTypes.CreateContainer")]
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public Moq.AutoMockingTest CreateMoq() => new MoqAutoMockingTest();

        [Benchmark(Description = "NSubstitute.NonPublicTypes.CreateContainer")]
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public NSubstitute.AutoMockingTest CreateNSubstitute() => new NSubstituteAutoMockingTest();
    }
}
