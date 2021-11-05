﻿using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
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
        protected AutoResolver(IKernel kernel) => Kernel = kernel;

        /// <summary>
        /// Maps target mock object to mocked object type.
        /// </summary>
        /// <param name="targetType">Target type for object to be converted to destination object.</param>
        /// <param name="context">Context of parent object to be resolved.</param>
        /// <returns></returns>
        public abstract object MapToTarget(Type targetType, CreationContext context);

        /// <inheritdoc />
        public virtual bool CanResolve(
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
        ) => MapToTarget(dependency.TargetType, context);
    }
}
