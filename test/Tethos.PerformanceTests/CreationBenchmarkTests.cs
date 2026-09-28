namespace Tethos.PerformanceTests;

using BenchmarkDotNet.Running;
using Shouldly;
using Tethos.Benchmarks;
using Tethos.PerformanceTests.Utils;
using Xunit;

[Collection("CreationBenchmark")]
public class CreationBenchmarkTests
{
    [Theory]
    [InlineData(800)]
    [Trait("Type", "Performance")]
    public void CreationBenchmark_Mean_ShouldBeBelowThreshold(int expected)
    {
        // Act
        var sut = BenchmarkRunner.Run<CreationBenchmark>();
        var means = sut.GetMeansInMilliseconds();

        // Assert
        means.ShouldAllBe(value => value < expected);
    }
}
