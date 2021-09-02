using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using System;

namespace Tethos
{
    /// <summary>
    /// Auto mocking resolver abstraction.
    /// </summary>
    public abstract class AutoResolver : ISubDependencyResolver
    {
        /// <summary>
        /// <see cref="Castle.Windsor"/> kernel dependency.
        /// </summary>
        public IKernel Kernel { get; }

        /// <summary>
        /// Constructor accepting <see cref="Castle.Windsor"/> kernel as dependency.
        /// </summary>
        public AutoResolver(IKernel kernel)
        {
            Kernel = kernel;
        }

        /// <summary>
        /// Mocking target type.
        /// </summary>
        public virtual Type DiamondType { get; }

        /// <summary>
        /// Maps target mock object to mocked object type.
        /// </summary>
        /// <param name="targetObject">Target Mock object to converted to destination object.</param>
        /// <returns></returns>
        public abstract object MapToTarget(object targetObject);

        /// <inheritdoc />
        public bool CanResolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency
        )
        {
            return dependency.TargetType.IsInterface;
        }

        /// <inheritdoc />
        public object Resolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency
        )
        {
            if (DiamondType == null)
            {
                return MapToTarget(dependency.TargetType);
            }

            var mockType = DiamondType.MakeGenericType(dependency.TargetType);
            return MapToTarget(Kernel.Resolve(mockType));
        }
    }
}
