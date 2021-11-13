using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using System;
using System.Linq;

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
        /// <param name="constructorArguments">Constructor arguments for non-abstract target type.</param>
        /// <returns>Auto-mocked object dependending on target type.</returns>
        public abstract object MapToTarget(Type targetType, Arguments constructorArguments);

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
        )
        {
            string GetType(object argument) =>
                argument.ToString()
                .Split(new string[] { "__" }, StringSplitOptions.None)
                .FirstOrDefault();
            var targetType = dependency.TargetType;
            var arguments = context.AdditionalArguments
                .Where(_ => !targetType.IsInterface)
                .Where(argument => GetType(argument.Key) == $"{targetType}");
            var constructorArguments = new Arguments().Add(arguments);

            return MapToTarget(targetType, constructorArguments);
        }
    }
}
