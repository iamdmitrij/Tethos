namespace Tethos.PerformanceTests
{
    using System.Linq;
    using BenchmarkDotNet.Running;
    using FluentAssertions;
    using Tethos.Benchmarks;
    using Tethos.PerformanceTests.Utils;
    using Xunit;

    public class ResolveFromBenchmarkTests
    {
        [Theory]
        [InlineData(5)]
        [Trait("Type", "Performance")]
        public void ResolveFromBenchmark_Mean_ShouldBeBelowThreshold(int expected)
        {
            // Act
            var sut = BenchmarkRunner.Run<ResolveFromBenchmark>();
            var means = sut.Reports.Select(report => report.ResultStatistics.Mean.ToMicroseconds());

            // Assert
            means.Should().OnlyContain(value => value < expected);
        }
    }
}
