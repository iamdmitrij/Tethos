namespace Tethos.Benchmarks
{
    using BenchmarkDotNet.Attributes;
    using global::Moq;
    using Tethos.Extensions;
    using Tethos.Tests.Common;

    public class ContainerBenchmark
    {
        public ContainerBenchmark()
        {
            this.ContainerFakeItEasy = FakeItEasy.AutoMocking.Create();
            this.ContainerMoq = Moq.AutoMocking.Create();
            this.ContainerNSubstitute = NSubstitute.AutoMocking.Create();
        }

        public IAutoMockingContainer ContainerFakeItEasy { get; }

        public IAutoMockingContainer ContainerMoq { get; }

        public IAutoMockingContainer ContainerNSubstitute { get; }

        [Benchmark(Description = "FakeItEasy.GetMockableViaProxy")]
        public IMockable GetMockableViaProxyContainerFakeItEasy() => this.ContainerFakeItEasy.Resolve<IMockable>();

        [Benchmark(Description = "FakeItEasy.ResolveFromSut")]
        public IMockable ResolveFromSutContainerFakeItEasy() => this.ContainerFakeItEasy.ResolveFrom<SystemUnderTest, IMockable>();

        [Benchmark(Description = "FakeItEasy.ResolveSut")]
        public SystemUnderTest ResolveSutContainerFakeItEasy() => this.ContainerFakeItEasy.Resolve<SystemUnderTest>();

        [Benchmark(Description = "Moq.GetMockableViaProxy")]
        public Mock<IMockable> GetMockableViaProxyMoq() => this.ContainerMoq.Resolve<Mock<IMockable>>();

        [Benchmark(Description = "Moq.ResolveFromSut")]
        public Mock<IMockable> ResolveFromSutMoq() => this.ContainerMoq.ResolveFrom<SystemUnderTest, Mock<IMockable>>();

        [Benchmark(Description = "Moq.ResolveSut")]
        public SystemUnderTest ResolveSutMoq() => this.ContainerMoq.Resolve<SystemUnderTest>();

        [Benchmark(Description = "NSubstitute.GetMockableViaProxy")]
        public IMockable GetMockableViaProxyNSubstitute() => this.ContainerNSubstitute.Resolve<IMockable>();

        [Benchmark(Description = "NSubstitute.ResolveFromSut")]
        public IMockable ResolveFromSutNSubstitute() => this.ContainerNSubstitute.ResolveFrom<SystemUnderTest, IMockable>();

        [Benchmark(Description = "NSubstitute.ResolveSut")]
        public SystemUnderTest ResolveSutNSubstituteNSubstitute() => this.ContainerNSubstitute.Resolve<SystemUnderTest>();
    }
}
