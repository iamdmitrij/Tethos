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
        public override object MapToMock(MappingArgument argument)
        {
            Action<IFakeOptions> arguments = argument.TargetType.IsInterface switch
            {
                true => options => _ = options,
                false => options => options.WithArgumentsForConstructor(argument.ConstructorArguments.Flatten()),
            };
            var mock = Create.Fake(argument.TargetType, arguments);
            var isPlainObject = !Fake.IsFake(argument.TargetObject ?? 0);

            if (isPlainObject)
            {
                this.Kernel.Register(Component.For(argument.TargetType)
                    .Instance(mock)
                    .OverridesExistingRegistration());
            }

            return mock;
        }
    }
}
