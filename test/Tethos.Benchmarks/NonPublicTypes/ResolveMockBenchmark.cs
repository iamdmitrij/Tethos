namespace Tethos.Benchmarks.NonPublicTypes
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
            this.ContainerFakeItEasy = new FakeItEasyAutoMockingTest().Container;
            this.ContainerMoq = new MoqAutoMockingTest().Container;
            this.ContainerNSubstitute = new NSubstituteAutoMockingTest().Container;
        }

        public IAutoMockingContainer ContainerFakeItEasy { get; }

        public IAutoMockingContainer ContainerMoq { get; }

        public IAutoMockingContainer ContainerNSubstitute { get; }

        [Benchmark(Description = "FakeItEasy.NonPublicTypes.GetMock")]
        public IMockable GetMockFakeItEasy() => this.ContainerFakeItEasy.Resolve<IMockable>();

        [Benchmark(Description = "Moq.NonPublicTypes.GetMock")]
        public IMockable GetMockMoq() => this.ContainerMoq.Resolve<Mock<IMockable>>().Object;

        [Benchmark(Description = "NSubstitute.NonPublicTypes.GetMock")]
        public IMockable GetMockNSubstitute() => this.ContainerNSubstitute.Resolve<IMockable>();
    }
}
