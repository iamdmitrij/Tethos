namespace Tethos.Tests.Benchmarks
{
    using BenchmarkDotNet.Running;

    internal static class Program
    {
        private static void Main()
        {
            _ = BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}
