namespace Tethos.FakeItEasy.Tests.Benchmarks
{
    using System.Diagnostics.CodeAnalysis;
    using BenchmarkDotNet.Attributes;
    using Tethos.Extensions;
    using Tethos.Tests.Common;

    public class ContainerBenchmark
    {
        public ContainerBenchmark()
        {
            this.Container = AutoMockingContainerFactory.Create();
        }

        public IAutoMockingContainer Container { get; }

        [Benchmark(Description = "FakeItEasy.GetMockableViaProxy")]
        public IMockable GetMockableViaProxy() => this.Container.Resolve<IMockable>();

        [Benchmark(Description = "FakeItEasy.ResolveFromSut")]
        public IMockable ResolveFromSut() => this.Container.ResolveFrom<SystemUnderTest, IMockable>();

        [Benchmark(Description = "FakeItEasy.ResolveSut")]
        public SystemUnderTest ResolveSut() => this.Container.Resolve<SystemUnderTest>();

        [Benchmark(Description = "FakeItEasy.StaticResolveSut")]
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public SystemUnderTest StaticResolveSut() => AutoMocking.Container.Resolve<SystemUnderTest>();
    }
}
