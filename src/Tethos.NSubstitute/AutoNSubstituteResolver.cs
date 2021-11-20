namespace Tethos.NSubstitute
{
    using System;
    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;
    using Castle.MicroKernel.Registration;
    using global::NSubstitute;

    /// <inheritdoc />
    internal class AutoNSubstituteResolver : AutoResolver
    {
        /// <inheritdoc cref="AutoResolver" />
        public AutoNSubstituteResolver(IKernel kernel)
            : base(kernel)
        {
        }

        /// <inheritdoc />
        public override bool CanResolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency) => dependency.TargetType.IsClass || base.CanResolve(context, contextHandlerResolver, model, dependency);

        /// <inheritdoc />
        public override object MapToTarget(Type targetType, Arguments constructorArguments)
        {
            var arguments = targetType.IsInterface switch
            {
                true => Array.Empty<object>(),
                false => constructorArguments.Flatten(),
            };
            var mock = Substitute.For(new Type[] { targetType }, arguments);

            this.Kernel.Register(Component.For(targetType)
                .Instance(mock)
                .OverridesExistingRegistration());

            return mock;
        }
    }
}
