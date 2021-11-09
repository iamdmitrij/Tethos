using Castle.MicroKernel;
using System;
using System.Linq;

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
        /// <typeparam name="T">Type for source object to be created.</typeparam>
        /// <typeparam name="K">Destination object type.</typeparam>
        /// <param name="arguments">Arguments used to contruct destination object.</param>
        /// <param name="name">Name of injected parameter.</param>
        /// <param name="value">Value of injected parameter.</param>
        /// <returns>Enriched arguments.</returns>
        public static Arguments AddDependencyTo<T, K>(this Arguments arguments, string name, K value)
            => arguments.AddDependencyTo(typeof(T), name, value);

        /// <summary>
        /// TODO: Comments
        /// </summary>
        /// <param name="arguments">Arguments used to contruct destination object.</param>
        /// <param name="sourceType">Type of source object.</param>
        /// <param name="name">Name of injected parameter.</param>
        /// <param name="value">Value of injected parameter.</param>
        /// <returns>Enriched arguments.</returns>
        public static Arguments AddDependencyTo(this Arguments arguments, Type sourceType, string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return arguments.AddNamed($"{sourceType}__{name}", value);
        }

        internal static object[] Flatten(this Arguments arguments) =>
            arguments.Select(argument => argument.Value).ToArray();
    }
}
