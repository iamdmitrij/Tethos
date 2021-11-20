using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using FakeItEasy.Creation;
using FakeItEasy.Sdk;
using System;

namespace Tethos.FakeItEasy
{
    /// <inheritdoc />
    internal class AutoFakeItEasyResolver : AutoResolver
    {
        /// <inheritdoc />
        public AutoFakeItEasyResolver(IKernel kernel) : base(kernel)
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
                true => options => { }
            };
            var mock = Create.Fake(targetType, arguments);

            Kernel.Register(Component.For(targetType)
                .Instance(mock)
                .OverridesExistingRegistration());

            return mock;
        }
    }
}
