namespace Tethos.FakeItEasy
{
    using System;
    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;
    using Castle.MicroKernel.Registration;
    using global::FakeItEasy.Creation;
    using global::FakeItEasy.Sdk;

    /// <inheritdoc />
    internal class AutoFakeItEasyResolver : AutoResolver
    {
        /// <inheritdoc cref="AutoResolver" />
        public AutoFakeItEasyResolver(IKernel kernel)
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
            Action<IFakeOptions> arguments = targetType.IsInterface switch
            {
                false => options => options.WithArgumentsForConstructor(constructorArguments.Flatten()),
                true => options => _ = options, // TODO: Visual Studio formatter goes nuts with the comma when using options => {},
            };
            var mock = Create.Fake(targetType, arguments);

            this.Kernel.Register(Component.For(targetType)
                .Instance(mock)
                .OverridesExistingRegistration());

            return mock;
        }
    }
}
