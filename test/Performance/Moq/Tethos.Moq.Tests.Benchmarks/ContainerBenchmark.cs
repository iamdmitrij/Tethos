namespace Tethos.Moq.Tests.Benchmarks
{
    using System.Diagnostics.CodeAnalysis;
    using BenchmarkDotNet.Attributes;
    using global::Moq;
    using Tethos.Extensions;
    using Tethos.Tests.Common;

    public class ContainerBenchmark
    {
        public ContainerBenchmark()
        {
            this.Container = AutoMocking.Create();
        }

        public IAutoMockingContainer Container { get; }

        [Benchmark(Description = "Moq.GetMockableViaProxy")]
        public IMockable GetMockableViaProxy() => this.Container.Resolve<IMockable>();

        [Benchmark(Description = "Moq.GetMockableViaMock")]
        public IMockable GetMockableViaMock() => this.Container.Resolve<Mock<IMockable>>().Object;

        [Benchmark(Description = "Moq.ResolveFromSut")]
        public IMockable ResolveFromSut() => this.Container.ResolveFrom<SystemUnderTest, IMockable>();

        [Benchmark(Description = "Moq.ResolveSut")]
        public SystemUnderTest ResolveSut() => this.Container.Resolve<SystemUnderTest>();

        [Benchmark(Description = "Moq.StaticResolveSut")]
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public SystemUnderTest StaticResolveSut() => AutoMocking.Container.Resolve<SystemUnderTest>();
    }
}
