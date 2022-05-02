namespace Tethos.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Order;
using global::Moq;
using Tethos.Extensions;
using Tethos.Tests.Common;

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn(NumeralSystem.Arabic)]
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

    [Benchmark(Description = "FakeItEasy.ResolveFrom")]
    public IMockable ResolveFromContainerFakeItEasy() => this.ContainerFakeItEasy.ResolveFrom<SystemUnderTest, IMockable>();

    [Benchmark(Description = "Moq.ResolveFrom")]
    public IMockable ResolveFromMoq() => this.ContainerMoq.ResolveFrom<SystemUnderTest, Mock<IMockable>>().Object;

    [Benchmark(Description = "NSubstitute.ResolveFrom")]
    public IMockable ResolveFromNSubstitute() => this.ContainerNSubstitute.ResolveFrom<SystemUnderTest, IMockable>();
}
