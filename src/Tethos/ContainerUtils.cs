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
        /// Add type for container mapping.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="arguments"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Arguments AddDependencyTo<T, K>(this Arguments arguments, string name, K value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return arguments.AddNamed($"{typeof(T)}__{name}", value);
        }
    }
}
