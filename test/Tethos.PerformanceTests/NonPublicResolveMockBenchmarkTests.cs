namespace Tethos.PerformanceTests
{
    using System.Linq;
    using BenchmarkDotNet.Running;
    using FluentAssertions;
    using Tethos.Benchmarks.NonPublicTypes;
    using Xunit;

    public class NonPublicResolveMockBenchmarkTests
    {
        [Fact]
        [Trait("Type", "Performance")]
        public void NonPublicResolveMockBenchmark_Mean_ShouldBeBelow5000()
        {
            // Arrange & Act
            var sut = BenchmarkRunner.Run<NonPublicResolveMockBenchmark>();
            var means = sut.Reports.Select(report => report.ResultStatistics.Mean.ToMicroseconds());

            // Assert
            means.Should().OnlyContain(value => value < 5);
        }
    }
}
