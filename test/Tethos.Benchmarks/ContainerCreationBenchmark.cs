namespace Tethos.Benchmarks
{
    using System.Diagnostics.CodeAnalysis;
    using BenchmarkDotNet.Attributes;

    public class ContainerCreationBenchmark
    {
        [Benchmark(Description = "FakeItEasy.MakeFactory")]
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public void MakeFactoryFakeItEasy() => FakeItEasy.AutoMocking.Create();

        [Benchmark(Description = "Moq.MakeFactory")]
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public void MakeFactoryMoq() => Moq.AutoMocking.Create();

        [Benchmark(Description = "NSubstitute.MakeFactory")]
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Framework requirement")]
        public void MakeFactoryNSubstitute() => NSubstitute.AutoMocking.Create();
    }
}
