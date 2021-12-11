namespace Tethos.Tests.Benchmarks
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Jobs;
    using global::Moq;
    using Tethos.Tests.Common;

    [MemoryDiagnoser]
    [RankColumn]
    [MinColumn]
    [MaxColumn]
    [Q1Column]
    [Q3Column]
    [AllStatisticsColumn]
    public class ExerciseBenchmark
    {
        public ExerciseBenchmark()
        {
            this.Container = Moq.AutoMockingContainerFactory.Create();
        }

        public Moq.IAutoMockingContainer Container { get; }

        [Benchmark]
        public void GetMockable()
        {
            var sut = this.Container.Resolve<SystemUnderTest>();
            var mock = this.Container.Resolve<Mock<IMockable>>();
            mock.Setup(m => m.Get()).Returns(0);
            sut.Exercise();
            mock.Verify();
        }

        [Benchmark]
        public void GetMockableProxy()
        {
            var sut = this.Container.Resolve<SystemUnderTest>();
            var mock = Mock.Get(this.Container.Resolve<IMockable>());
            mock.Setup(m => m.Get()).Returns(0);
            sut.Exercise();
            mock.Verify();
        }
    }
}