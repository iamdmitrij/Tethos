namespace Tethos.Extensions
{
    using System;
    using System.Linq;
    using Castle.MicroKernel;

    /// <summary>
    /// A set of util functions helping to extend <see cref="Castle.Windsor.IWindsorContainer"/> for auto-mocking.
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Add type for container mapping.
        /// </summary>
        /// <typeparam name="TSource">Type for source object to be created.</typeparam>
        /// <typeparam name="TDestination">Destination object type.</typeparam>
        /// <param name="arguments">Arguments used to contruct destination object.</param>
        /// <param name="name">Name of injected parameter.</param>
        /// <param name="value">Value of injected parameter.</param>
        /// <returns>Enriched arguments.</returns>
        public static Arguments AddDependencyTo<TSource, TDestination>(this Arguments arguments, string name, TDestination value)
            => arguments.AddDependencyTo(typeof(TSource), name, value);

        /// <summary>
        /// Add type for container mapping.
        /// </summary>
        /// <param name="arguments">Arguments used to contruct destination object.</param>
        /// <param name="sourceType">Type of source object.</param>
        /// <param name="name">Name of injected parameter.</param>
        /// <param name="value">Value of injected parameter.</param>
        /// <returns>Enriched arguments.</returns>
        public static Arguments AddDependencyTo(this Arguments arguments, Type sourceType, string name, object value) =>
            name switch
            {
                var parameterName when string.IsNullOrWhiteSpace(parameterName) => throw new ArgumentNullException(nameof(name)),
                var parameterName => arguments.AddNamed($"{sourceType}__{parameterName}", value),
            };

        /// <summary>
        /// Resolve child depedency within parent dependency.
        /// </summary>
        /// <typeparam name="TParent">Parent type to use.</typeparam>
        /// <typeparam name="TChild">Child type to resolve.</typeparam>
        /// <param name="container">Auto-mocking container instance.</param>
        /// <returns>Child type resolved instance.</returns>
        public static TChild ResolveFrom<TParent, TChild>(this IAutoMockingContainer container)
            => (TChild)container.ResolveFrom(typeof(TParent), typeof(TChild));

        /// <summary>
        /// Resolve child depedency within parent dependency.
        /// </summary>
        /// <param name="container">Auto-mocking container instance.</param>
        /// <param name="parent">Parent type of an object.</param>
        /// <param name="child">Child type of an object.</param>
        /// <returns>Child type resolved object.</returns>
        public static object ResolveFrom(this IAutoMockingContainer container, Type parent, Type child)
        {
            container.Resolve(parent);
            return container.Resolve(child);
        }

        internal static object[] Flatten(this Arguments arguments) =>
            arguments.Select(argument => argument.Value).ToArray();
    }
}
