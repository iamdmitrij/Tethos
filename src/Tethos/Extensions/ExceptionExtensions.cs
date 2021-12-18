namespace Tethos.Extensions
{
    using System;
    using System.Linq;

    internal static class ExceptionExtensions
    {
        internal static T SwallowExceptions<T>(this Func<T> func, params Type[] types)
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception exception) when (types?.Contains(exception.GetType()) ?? false)
            {
                return default;
            }
        }

        internal static bool Throws<T>(this Func<T> func, params Type[] types)
        {
            try
            {
                _ = func.Invoke();
            }
            catch (Exception exception)
            {
                return types?.Contains(exception.GetType()) ?? false;
            }

            return false;
        }
    }
}
