namespace Tethos.NSubstitute.Tests.Benchmarks
{
    using BenchmarkDotNet.Attributes;
    using Tethos.Extensions;
    using Tethos.Tests.Common;

    [BenchmarkCategory("NSubstitute")]
    public class ContainerBenchmark
    {
        public ContainerBenchmark()
        {
            this.Container = AutoMockingContainerFactory.Create();
        }

        public IAutoMockingContainer Container { get; }

        [Benchmark(Description = "NSubstitute")]
        public IMockable GetMockableViaProxy() => this.Container.Resolve<IMockable>();

        [Benchmark(Description = "NSubstitute")]
        public IMockable ResolveFromSut() => this.Container.ResolveFrom<SystemUnderTest, IMockable>();

        [Benchmark(Description = "NSubstitute")]
        public SystemUnderTest ResolveSut() => this.Container.Resolve<SystemUnderTest>();
    }
}
