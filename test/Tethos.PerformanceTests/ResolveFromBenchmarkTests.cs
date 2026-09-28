namespace Tethos.PerformanceTests;

using BenchmarkDotNet.Running;
using Shouldly;
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
        var means = sut.GetMeansInMicroseconds();

        // Assert
        means.ShouldAllBe(value => value < expected);
    }
}
