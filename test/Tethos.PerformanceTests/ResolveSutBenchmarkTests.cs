namespace Tethos.PerformanceTests;

using BenchmarkDotNet.Running;
using Shouldly;
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
        means.ShouldAllBe(value => value < expected);
    }
}
