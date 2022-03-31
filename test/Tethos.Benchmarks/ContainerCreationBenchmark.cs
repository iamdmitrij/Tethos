namespace Tethos.NSubstitute.Tests.Benchmarks
{
    using System.Diagnostics.CodeAnalysis;
    using BenchmarkDotNet.Attributes;

    [ShortRunJob]
    public class ContainerCreationBenchmark
    {
        [Benchmark(Description = "NSubstitute.MakeFactory")]
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public void MakeFactoryMoq() => Moq.AutoMocking.Create();

        [Benchmark(Description = "NSubstitute.MakeFactory")]
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public void MakeFactoryFakeItEasy() => FakeItEasy.AutoMocking.Create();

        [Benchmark(Description = "NSubstitute.MakeFactory")]
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public void MakeFactoryNSubstitute() => NSubstitute.AutoMocking.Create();
    }
}
