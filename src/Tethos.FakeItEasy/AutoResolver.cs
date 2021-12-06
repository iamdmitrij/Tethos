namespace Tethos.FakeItEasy
{
    using System;
    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;
    using Castle.MicroKernel.Registration;
    using global::FakeItEasy;
    using global::FakeItEasy.Creation;
    using global::FakeItEasy.Sdk;
    using Tethos.Extensions;

    /// <inheritdoc />
    internal class AutoResolver : BaseAutoResolver
    {
        /// <inheritdoc cref="BaseAutoResolver" />
        public AutoResolver(IKernel kernel)
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
        public override object MapToMock(Type targetType, object targetObject, Arguments constructorArguments)
        {
            Action<IFakeOptions> arguments = targetType.IsInterface switch
            {
                false => options => options.WithArgumentsForConstructor(constructorArguments.Flatten()),

                // TODO: Visual Studio formatter goes nuts with the comma when using options => {},
                true => options => _ = options,
            };
            var mock = Create.Fake(targetType, arguments);
            var isPlainObject = !Fake.IsFake(targetObject ?? 0);
            if (isPlainObject)
            {
                this.Kernel.Register(Component.For(targetType)
                    .Instance(mock)
                    .OverridesExistingRegistration());
            }

            return mock;
        }
    }
}
