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

        [Benchmark(Description = "FakeItEasy.GetMock")]
        public IMockable GetMockFakeItEasy() => this.ContainerFakeItEasy.Resolve<IMockable>();

        [Benchmark(Description = "Moq.GetMock")]
        public IMockable GetMockMoq() => this.ContainerMoq.Resolve<Mock<IMockable>>().Object;

        [Benchmark(Description = "NSubstitute.GetMock")]
        public IMockable GetMockNSubstitute() => this.ContainerNSubstitute.Resolve<IMockable>();
    }
}
