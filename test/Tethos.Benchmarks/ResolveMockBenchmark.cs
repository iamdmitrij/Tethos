namespace Tethos.Benchmarks
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Mathematics;
    using BenchmarkDotNet.Order;
    using global::Moq;
    using Tethos.Tests.Common;

    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn(NumeralSystem.Arabic)]
    public class ResolveMockBenchmark
    {
        public ResolveMockBenchmark()
        {
            this.ContainerFakeItEasy = FakeItEasy.AutoMocking.Create();
            this.ContainerMoq = Moq.AutoMocking.Create();
            this.ContainerNSubstitute = NSubstitute.AutoMocking.Create();
        }

        public IAutoMockingContainer ContainerFakeItEasy { get; }

        public IAutoMockingContainer ContainerMoq { get; }

        public IAutoMockingContainer ContainerNSubstitute { get; }

        [Benchmark(Description = "FakeItEasy.GetMockableViaProxy")]
        public IMockable GetMockableViaProxyContainerFakeItEasy() => this.ContainerFakeItEasy.Resolve<IMockable>();

        [Benchmark(Description = "Moq.GetMockableViaProxy")]
        public Mock<IMockable> GetMockableViaProxyMoq() => this.ContainerMoq.Resolve<Mock<IMockable>>();

        [Benchmark(Description = "NSubstitute.GetMockableViaProxy")]
        public IMockable GetMockableViaProxyNSubstitute() => this.ContainerNSubstitute.Resolve<IMockable>();
    }
}
