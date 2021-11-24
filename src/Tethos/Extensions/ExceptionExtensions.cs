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
            catch (Exception ex) when (types.Contains(ex.GetType()))
            {
                return default;
            }
        }
    }
}
