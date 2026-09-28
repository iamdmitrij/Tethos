namespace Tethos.PerformanceTests;

using BenchmarkDotNet.Running;
using Shouldly;
using Tethos.Benchmarks;
using Tethos.PerformanceTests.Utils;
using Xunit;

public class ResolveMockBenchmarkTests
{
    [Theory]
    [InlineData(5)]
    [Trait("Type", "Performance")]
    public void ResolveMockBenchmark_Mean_ShouldBeBelowThreshold(int expected)
    {
        // Act
        var sut = BenchmarkRunner.Run<ResolveMockBenchmark>();
        var means = sut.GetMeansInMicroseconds();

        // Assert
        means.ShouldAllBe(value => value < expected);
    }
}
