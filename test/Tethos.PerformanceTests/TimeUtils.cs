namespace Tethos.PerformanceTests
{
    public static class TimeUtils
    {
        public static double ToMilliseconds(this double nanoSeconds) => nanoSeconds * 0.000001;
    }
}
