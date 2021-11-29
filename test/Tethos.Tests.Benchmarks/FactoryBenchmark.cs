namespace Tethos.Tests.Benchmarks
{
    using BenchmarkDotNet.Attributes;
    using global::Moq;
    using Tethos.Tests.Common;

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
    }
}