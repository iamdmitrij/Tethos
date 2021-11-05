using Castle.MicroKernel;
using System;

namespace Tethos
{
    /// <summary>
    /// A set of utils function helping to extend <see cref="Castle.Windsor.IWindsorContainer"/> for auto-mocking.
    /// </summary>
    public static class ContainerUtils
    {
        /// <summary>
        /// TODO: Comments
        /// Add type for container mapping.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="arguments"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Arguments AddDependencyTo<T, K>(this Arguments arguments, string name, K value)
            => arguments.AddDependencyTo(typeof(T), name, value);

        /// <summary>
        /// TODO: Comments
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="sourceType"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Arguments AddDependencyTo(this Arguments arguments, Type sourceType, string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return arguments.AddNamed($"{sourceType}__{name}", value);
        }
    }
}
