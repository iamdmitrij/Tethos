namespace Tethos.Benchmarks
{
    using System.Diagnostics.CodeAnalysis;
    using BenchmarkDotNet.Attributes;
    using Tethos.Tests.Common;

    public class StaticContainerBenchmark
    {
        [Benchmark(Description = "Moq.StaticResolveSut")]
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public SystemUnderTest StaticResolveSutMoq() => Moq.AutoMocking.Container.Resolve<SystemUnderTest>();

        [Benchmark(Description = "NSubstitute.StaticResolveSut")]
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public SystemUnderTest StaticResolveSutNSubstitute() => NSubstitute.AutoMocking.Container.Resolve<SystemUnderTest>();

        [Benchmark(Description = "FakeItEasy.StaticResolveSut")]
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public SystemUnderTest StaticResolveSutFakeItEasy() => FakeItEasy.AutoMocking.Container.Resolve<SystemUnderTest>();
    }
}
