namespace Tethos.PerformanceTests
{
    using System.Linq;
    using BenchmarkDotNet.Running;
    using FluentAssertions;
    using Tethos.Benchmarks;
    using Xunit;

    public class ResolveMockBenchmarkTests
    {
        [Fact]
        [Trait("Type", "Performance")]
        public void ResolveMockBenchmark_Mean_ShouldBeBelow5()
        {
            // Arrange & Act
            var sut = BenchmarkRunner.Run<ResolveMockBenchmark>();
            var means = sut.Reports.Select(report => report.ResultStatistics.Mean.ToMicroseconds());

            // Assert
            means.Should().OnlyContain(value => value < 5);
        }
    }
}
