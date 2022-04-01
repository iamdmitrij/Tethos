namespace Tethos.PerformanceTests
{
    using System.Linq;
    using BenchmarkDotNet.Running;
    using FluentAssertions;
    using Tethos.Benchmarks;
    using Xunit;

    public class ResolveFromBenchmarkTests
    {
        [Fact]
        [Trait("Type", "Performance")]
        public void ResolveFromBenchmark_Mean_ShouldBeBelow5()
        {
            // Arrange & Act
            var sut = BenchmarkRunner.Run<ResolveFromBenchmark>();
            var means = sut.Reports.Select(report => report.ResultStatistics.Mean.ToMicroseconds());

            // Assert
            means.Should().OnlyContain(value => value < 5);
        }
    }
}
