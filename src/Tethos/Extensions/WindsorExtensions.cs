namespace Tethos.Extensions
{
    using System;
    using Castle.MicroKernel.Registration;

    /// <summary>
    /// TODO: Docs
    /// </summary>
    public static class WindsorExtensions
    {
        /// <summary>
        /// TODO: Docs
        /// </summary>
        /// <typeparam name="TParent"></typeparam>
        /// <typeparam name="TChild"></typeparam>
        /// <param name="container"></param>
        /// <returns></returns>
        public static TChild ResolveFrom<TParent, TChild>(this IAutoMockingContainer container)
        {
            container.Resolve<TParent>();
            return container.Resolve<TChild>();
        }

        internal static ComponentRegistration<T> OverridesExistingRegistration<T>(
            this ComponentRegistration<T> componentRegistration)
            where T : class => componentRegistration?.Named($"{Guid.NewGuid()}").IsDefault();
    }
}
