namespace Tethos.PerformanceTests
{
    using System.Linq;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Reports;
    using BenchmarkDotNet.Running;
    using FluentAssertions;
    using Tethos.Benchmarks;
    using Xunit;

    public class StaticContainerBenchmarkTests
    {
        private readonly Summary summary;

        public StaticContainerBenchmarkTests()
        {
            this.summary = BenchmarkRunner.Run(typeof(StaticContainerBenchmark), new DebugBuildConfig());
        }

        [Fact]
        [Trait("Type", "Performance")]
        public void StaticContainerBenchmark_Mean_ShouldBeBelow600()
        {
            // Arrange & Act
            var means = this.summary.Reports.Select(report => report.ResultStatistics.Mean.ToMicroseconds());

            // Assert
            means.Should().OnlyContain(x => x < 100);
        }
    }
}
