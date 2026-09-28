namespace Tethos.PerformanceTests.NonPublicTypes;

using BenchmarkDotNet.Running;
using Shouldly;
using Tethos.Benchmarks.NonPublicTypes;
using Tethos.PerformanceTests.Utils;
using Xunit;

public class CreationBenchmarkTests
{
    [Theory]
    [InlineData(15000)]
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
