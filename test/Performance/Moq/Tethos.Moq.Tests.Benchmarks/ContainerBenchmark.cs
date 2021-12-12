namespace Tethos.Moq.Tests.Benchmarks
{
    using BenchmarkDotNet.Attributes;
    using global::Moq;
    using Tethos.Extensions;
    using Tethos.Tests.Common;

    [BenchmarkCategory("Moq")]
    public class ContainerBenchmark
    {
        public ContainerBenchmark()
        {
            this.Container = AutoMockingContainerFactory.Create();
        }

        public IAutoMockingContainer Container { get; }

        [Benchmark]
        public IMockable GetMockableViaProxy() => this.Container.Resolve<IMockable>();

        [Benchmark]
        public IMockable GetMockableViaMock() => this.Container.Resolve<Mock<IMockable>>().Object;

        [Benchmark]
        public IMockable ResolveFromSut() => this.Container.ResolveFrom<SystemUnderTest, IMockable>();

        [Benchmark]
        public SystemUnderTest ResolveSut() => this.Container.Resolve<SystemUnderTest>();
    }
}