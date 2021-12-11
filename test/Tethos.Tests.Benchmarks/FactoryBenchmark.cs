namespace Tethos.Tests.Benchmarks
{
    using BenchmarkDotNet.Attributes;
    using global::Moq;
    using Tethos.Tests.Common;

    [MemoryDiagnoser]
    [RankColumn]
    [MinColumn]
    [MaxColumn]
    [Q1Column]
    [Q3Column]
    [AllStatisticsColumn]
    public class FactoryBenchmark
    {
        public FactoryBenchmark()
        {
            this.Container = Moq.AutoMockingContainerFactory.Create();
        }

        public Moq.IAutoMockingContainer Container { get; }

        [Benchmark]
        public IMockable GetMockable() => this.Container.Resolve<IMockable>();

        [Benchmark]
        public Mock<IMockable> GetMockOfMockable() => this.Container.Resolve<Mock<IMockable>>();

        //[Benchmark]
        //public IAutoMockingContainer GetFactory() => Moq.AutoMockingContainerFactory.Create();
    }
}