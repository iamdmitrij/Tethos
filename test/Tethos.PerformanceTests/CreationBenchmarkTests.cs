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
        public void CreationBenchmark_Mean_ShouldBeBelow600()
        {
            // Arrange & Act
            var actual = this.summary.Reports.Select(report => report.ResultStatistics.Mean);

            // Assert
            actual.Should().OnlyContain(x => x < 600);
        }
    }
}
