namespace Tethos.FakeItEasy.Tests.Benchmarks
{
    using BenchmarkDotNet.Attributes;
    using Tethos.Extensions;
    using Tethos.Tests.Common;

    [BenchmarkCategory("FakeItEasy")]
    public class ContainerBenchmark
    {
        public ContainerBenchmark()
        {
            this.Container = AutoMockingContainerFactory.Create();
        }

        public IAutoMockingContainer Container { get; }

        public IMockable GetMockableViaProxy() => this.Container.Resolve<IMockable>();

        [Benchmark]
        public IMockable ResolveFromSut() => this.Container.ResolveFrom<SystemUnderTest, IMockable>();

        [Benchmark]
        public SystemUnderTest ResolveSut() => this.Container.Resolve<SystemUnderTest>();
    }
}