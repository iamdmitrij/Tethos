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
    internal abstract class AutoResolver : ISubDependencyResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoResolver"/> class.
        /// </summary>
        /// <param name="kernel">Reference to Castle Container.</param>
        protected AutoResolver(IKernel kernel) => this.Kernel = kernel;

        /// <summary>
        /// Gets <see cref="Castle.Windsor"/> kernel dependency.
        /// </summary>
        public IKernel Kernel { get; }

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
            DependencyModel dependency) => dependency.TargetType.IsInterface;

        /// <inheritdoc />
        public object Resolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency)
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

            return this.MapToTarget(targetType, constructorArguments);
        }
    }
}
