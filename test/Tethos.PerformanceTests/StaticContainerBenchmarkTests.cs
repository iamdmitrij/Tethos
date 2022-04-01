namespace Tethos.PerformanceTests
{
    using System.Linq;
    using BenchmarkDotNet.Running;
    using FluentAssertions;
    using Tethos.Benchmarks;
    using Xunit;

    public class StaticContainerBenchmarkTests
    {
        [Fact]
        [Trait("Type", "Performance")]
        public void StaticContainerBenchmark_Mean_ShouldBeBelow600()
        {
            // Arrange & Act
            var summary = BenchmarkRunner.Run<CreationBenchmark>();
            var means = summary.Reports.Select(report => report.ResultStatistics.Mean.ToMicroseconds());

            // Assert
            means.Should().OnlyContain(x => x < 100);
        }
    }
}
