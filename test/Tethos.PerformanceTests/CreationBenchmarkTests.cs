﻿namespace Tethos.PerformanceTests
{
    using System.Linq;
    using BenchmarkDotNet.Running;
    using FluentAssertions;
    using Tethos.Benchmarks;
    using Xunit;

    public class CreationBenchmarkTests
    {
        [Theory]
        [InlineData(600)]
        [Trait("Type", "Performance")]
        public void CreationBenchmark_Mean_ShouldBeBelowThreshold(int expected)
        {
            // Act
            var sut = BenchmarkRunner.Run<CreationBenchmark>();
            var means = sut.Reports.Select(report => report.ResultStatistics.Mean.ToMilliseconds());

            // Assert
            means.Should().OnlyContain(value => value < expected);
        }
    }
}
