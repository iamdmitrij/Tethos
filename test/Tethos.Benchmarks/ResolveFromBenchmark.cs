namespace Tethos.Benchmarks
{
    using BenchmarkDotNet.Attributes;
    using global::Moq;
    using Tethos.Extensions;
    using Tethos.Tests.Common;

    public class ResolveFromBenchmark
    {
        public ResolveFromBenchmark()
        {
            this.ContainerFakeItEasy = FakeItEasy.AutoMocking.Create();
            this.ContainerMoq = Moq.AutoMocking.Create();
            this.ContainerNSubstitute = NSubstitute.AutoMocking.Create();
        }

        public IAutoMockingContainer ContainerFakeItEasy { get; }

        public IAutoMockingContainer ContainerMoq { get; }

        public IAutoMockingContainer ContainerNSubstitute { get; }

        [Benchmark(Description = "FakeItEasy.ResolveFromSut")]
        public IMockable ResolveFromSutContainerFakeItEasy() => this.ContainerFakeItEasy.ResolveFrom<SystemUnderTest, IMockable>();

        [Benchmark(Description = "Moq.ResolveFromSut")]
        public Mock<IMockable> ResolveFromSutMoq() => this.ContainerMoq.ResolveFrom<SystemUnderTest, Mock<IMockable>>();

        [Benchmark(Description = "NSubstitute.ResolveFromSut")]
        public IMockable ResolveFromSutNSubstitute() => this.ContainerNSubstitute.ResolveFrom<SystemUnderTest, IMockable>();
    }
}
