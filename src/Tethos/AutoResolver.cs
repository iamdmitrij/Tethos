namespace Tethos
{
    using System;
    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;

    /// <summary>
    /// Auto mocking resolver abstraction.
    /// </summary>
    public abstract class AutoResolver : ISubDependencyResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoResolver"/> class.
        /// </summary>
        /// <param name="kernel"><see cref="Castle.Windsor"/> kernel setup.</param>
        protected AutoResolver(IKernel kernel) => this.Kernel = kernel;

        /// <summary>
        /// Gets <see cref="Castle.Windsor"/> kernel dependency.
        /// </summary>
        public IKernel Kernel { get; }

        /// <summary>
        /// Maps target mock object to mocked object type.
        /// </summary>
        /// <param name="targetType">Target type for object to be converted to destination object.</param>
        /// <returns>Object converted to mock type.</returns>
        public abstract object MapToTarget(Type targetType);

        /// <inheritdoc />
        public bool CanResolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency) => dependency.TargetType.IsInterface;

        /// <inheritdoc />
        public object Resolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency) => this.MapToTarget(dependency.TargetType);
    }
}
