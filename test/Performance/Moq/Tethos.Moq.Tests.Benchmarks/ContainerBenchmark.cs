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

        [Benchmark(Description = "Moq")]
        public IMockable GetMockableViaProxy() => this.Container.Resolve<IMockable>();

        [Benchmark(Description = "Moq")]
        public IMockable GetMockableViaMock() => this.Container.Resolve<Mock<IMockable>>().Object;

        [Benchmark(Description = "Moq")]
        public IMockable ResolveFromSut() => this.Container.ResolveFrom<SystemUnderTest, IMockable>();

        [Benchmark(Description = "Moq")]
        public SystemUnderTest ResolveSut() => this.Container.Resolve<SystemUnderTest>();
    }
}
