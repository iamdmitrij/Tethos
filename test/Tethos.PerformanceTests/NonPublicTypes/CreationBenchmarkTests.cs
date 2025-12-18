namespace Tethos.PerformanceTests.NonPublicTypes;

using BenchmarkDotNet.Running;
using FluentAssertions;
using Tethos.Benchmarks.NonPublicTypes;
using Tethos.PerformanceTests.Utils;
using Xunit;

public class CreationBenchmarkTests
{
    [Theory]
    [InlineData(10000)]
    [Trait("Type", "Performance")]
    public void CreationBenchmark_Mean_ShouldBeBelowThreshold(int expected)
    {
        // Act
        var sut = BenchmarkRunner.Run<CreationBenchmark>();
        var means = sut.GetMeansInMilliseconds();

        // Assert
        means.Should().OnlyContain(value => value < expected);
    }
}
