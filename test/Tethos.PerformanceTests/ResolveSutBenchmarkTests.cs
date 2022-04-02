namespace Tethos.PerformanceTests
{
    using System.Linq;
    using BenchmarkDotNet.Running;
    using FluentAssertions;
    using Tethos.Benchmarks;
    using Tethos.PerformanceTests.Utils;
    using Xunit;

    public class ResolveSutBenchmarkTests
    {
        [Theory]
        [InlineData(5)]
        [Trait("Type", "Performance")]
        public void ResolveSutBenchmark_Mean_ShouldBeBelowThreshold(int expected)
        {
            // Act
            var sut = BenchmarkRunner.Run<ResolveSutBenchmark>();
            var means = sut.GetMeansInMicroseconds();

            // Assert
            means.Should().OnlyContain(value => value < expected);
        }
    }
}
