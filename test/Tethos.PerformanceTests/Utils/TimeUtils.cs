namespace Tethos.PerformanceTests.Utils
{
    public static class TimeUtils
    {
        public static double ToMilliseconds(this double nanoSeconds) => nanoSeconds * 0.000001;

        public static double ToMicroseconds(this double nanoSeconds) => nanoSeconds * 0.001;
    }
}
