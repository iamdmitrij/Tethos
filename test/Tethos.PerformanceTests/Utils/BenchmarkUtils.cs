namespace Tethos.PerformanceTests.Utils
{
    using System.Linq;
    using BenchmarkDotNet.Reports;

    public static class BenchmarkUtils
    {
        public static double[] GetMeansInMilliseconds(this Summary summary) =>
            summary.Reports
                .Select(report => report.ResultStatistics.Mean.ToMilliseconds())
                .ToArray();

        public static double[] GetMeansInMicroseconds(this Summary summary) =>
            summary.Reports
                .Select(report => report.ResultStatistics.Mean.ToMicroseconds())
                .ToArray();
    }
}
