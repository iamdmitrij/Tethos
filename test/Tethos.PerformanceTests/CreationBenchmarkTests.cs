namespace Tethos.PerformanceTests;

using BenchmarkDotNet.Running;
using FluentAssertions;
using Tethos.Benchmarks;
using Tethos.PerformanceTests.Utils;
using Xunit;

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
        means.Should().OnlyContain(value => value < expected);
    }
}
