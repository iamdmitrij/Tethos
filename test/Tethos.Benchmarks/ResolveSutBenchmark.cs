namespace Tethos.Benchmarks
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Mathematics;
    using BenchmarkDotNet.Order;
    using Tethos.Tests.Common;

    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn(NumeralSystem.Arabic)]
    public class ResolveSutBenchmark
    {
        public ResolveSutBenchmark()
        {
            this.ContainerFakeItEasy = FakeItEasy.AutoMocking.Create();
            this.ContainerMoq = Moq.AutoMocking.Create();
            this.ContainerNSubstitute = NSubstitute.AutoMocking.Create();
        }

        public IAutoMockingContainer ContainerFakeItEasy { get; }

        public IAutoMockingContainer ContainerMoq { get; }

        public IAutoMockingContainer ContainerNSubstitute { get; }

        [Benchmark(Description = "FakeItEasy.ResolveSut")]
        public SystemUnderTest GetSutFakeItEasy() => this.ContainerFakeItEasy.Resolve<SystemUnderTest>();

        [Benchmark(Description = "Moq.ResolveSut")]
        public SystemUnderTest GetSutMoq() => this.ContainerMoq.Resolve<SystemUnderTest>();

        [Benchmark(Description = "NSubstitute.ResolveSut")]
        public SystemUnderTest GetSutNSubstitute() => this.ContainerNSubstitute.Resolve<SystemUnderTest>();
    }
}
