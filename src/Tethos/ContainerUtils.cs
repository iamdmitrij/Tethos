using Castle.MicroKernel;

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
        public static Arguments AddDependencyTo<T, K>(this Arguments arguments, string name, K value) => arguments.AddNamed($"{typeof(T)}__{name}", value);
    }
}
