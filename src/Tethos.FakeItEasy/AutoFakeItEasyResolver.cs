namespace Tethos.FakeItEasy
{
    using System;
    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;
    using Castle.MicroKernel.Handlers;
    using Castle.MicroKernel.Registration;
    using global::FakeItEasy;
    using global::FakeItEasy.Creation;
    using global::FakeItEasy.Sdk;
    using Tethos.Extensions;

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

                // TODO: Visual Studio formatter goes nuts with the comma when using options => {},
                true => options => _ = options,
            };
            var mock = Create.Fake(targetType, arguments);
            var func = () => this.Kernel.Resolve(targetType);
            var currentObject = func.SwallowExceptions(typeof(ComponentNotFoundException), typeof(HandlerException)) ?? 0;

            if (!Fake.IsFake(currentObject))
            {
                this.Kernel.Register(Component.For(targetType)
                    .Instance(mock)
                    .OverridesExistingRegistration());
            }

            return mock;
        }
    }
}
