namespace Tethos.PerformanceTests.NonPublicTypes
{
    using System.Linq;
    using BenchmarkDotNet.Running;
    using FluentAssertions;
    using Tethos.Benchmarks.NonPublicTypes;
    using Tethos.PerformanceTests.Utils;
    using Xunit;

    public class ResolveMockBenchmarkTests
    {
        [Theory]
        [InlineData(5)]
        [Trait("Type", "Performance")]
        public void NonPublicResolveMockBenchmark_Mean_ShouldBeBelowThreshold(int expected)
        {
            // Act
            var sut = BenchmarkRunner.Run<ResolveMockBenchmark>();
            var means = sut.GetMeansInMicroseconds();

            // Assert
            means.Should().OnlyContain(value => value < expected);
        }
    }
}
