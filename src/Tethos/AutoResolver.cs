﻿namespace Tethos
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
        /// <see cref="Castle.Windsor"/> kernel dependency.
        /// </summary>
        public IKernel Kernel { get; }

        /// <summary>
        /// Constructor accepting <see cref="Castle.Windsor"/> kernel as dependency.
        /// </summary>
        protected AutoResolver(IKernel kernel) => this.Kernel = kernel;

        /// <summary>
        /// Maps target mock object to mocked object type.
        /// </summary>
        /// <param name="targetType">Target type for object to be converted to destination object.</param>
        /// <returns></returns>
        public abstract object MapToTarget(Type targetType);

        /// <inheritdoc />
        public bool CanResolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency
        ) => dependency.TargetType.IsInterface;

        /// <inheritdoc />
        public object Resolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency
        ) => this.MapToTarget(dependency.TargetType);
    }
}
