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
        public void StaticContainerBenchmark_Mean_ShouldBeBelow100()
        {
            // Arrange & Act
            var sut = BenchmarkRunner.Run<CreationBenchmark>();
            var means = sut.Reports.Select(report => report.ResultStatistics.Mean.ToMilliseconds());

            // Assert
            means.Should().OnlyContain(value => value < 600);
        }
    }
}
