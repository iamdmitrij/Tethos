namespace Tethos.PerformanceTests
{
    using System.Linq;
    using BenchmarkDotNet.Running;
    using FluentAssertions;
    using Tethos.Benchmarks.NonPublicTypes;
    using Xunit;

    public class NonPublicCreationBenchmarkTests
    {
        [Fact]
        [Trait("Type", "Performance")]
        public void CreationBenchmark_Mean_ShouldBeBelow5000()
        {
            // Arrange & Act
            var sut = BenchmarkRunner.Run<NonPublicCreationBenchmark>();
            var means = sut.Reports.Select(report => report.ResultStatistics.Mean.ToMilliseconds());

            // Assert
            means.Should().OnlyContain(value => value < 5000);
        }
    }
}
