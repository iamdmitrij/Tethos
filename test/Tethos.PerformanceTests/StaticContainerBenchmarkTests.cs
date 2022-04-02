namespace Tethos.PerformanceTests
{
    using System.Linq;
    using BenchmarkDotNet.Running;
    using FluentAssertions;
    using Tethos.Benchmarks;
    using Tethos.PerformanceTests.Utils;
    using Xunit;

    public class StaticContainerBenchmarkTests
    {
        [Theory]
        [InlineData(600)]
        [Trait("Type", "Performance")]
        public void StaticContainerBenchmark_Mean_ShouldBeBelowThreshold(int expected)
        {
            // Act
            var sut = BenchmarkRunner.Run<CreationBenchmark>();
            var means = sut.GetMeansInMilliseconds();

            // Assert
            means.Should().OnlyContain(value => value < expected);
        }
    }
}
