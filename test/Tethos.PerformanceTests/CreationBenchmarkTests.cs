namespace Tethos.PerformanceTests
{
    using System.Linq;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Reports;
    using BenchmarkDotNet.Running;
    using FluentAssertions;
    using Tethos.Benchmarks;
    using Xunit;

    public class CreationBenchmarkTests
    {
        private readonly Summary summary;

        public CreationBenchmarkTests()
        {
            this.summary = BenchmarkRunner.Run(typeof(CreationBenchmark), new DebugBuildConfig());
        }

        [Fact]
        [Trait("Type", "Performance")]
        public void CreationBenchmark_Mean_ShouldBeBelow600()
        {
            // Arrange & Act
            var means = this.summary.Reports.Select(report => report.ResultStatistics.Mean.ToMilliseconds());

            // Assert
            means.Should().OnlyContain(x => x < 600);
        }
    }
}
