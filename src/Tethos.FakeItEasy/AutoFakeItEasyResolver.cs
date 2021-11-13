using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using FakeItEasy.Sdk;
using System;

namespace Tethos.FakeItEasy
{
    /// <summary>
    /// <see cref="AutoResolver"/> tailored for <see cref="FakeItEasy"/> mocking systems.
    /// </summary>
    public class AutoFakeItEasyResolver : AutoResolver
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
            DependencyModel dependency
        ) => dependency.TargetType.IsClass || base.CanResolve(context, contextHandlerResolver, model, dependency);

        /// <inheritdoc />
        public override object MapToTarget(Type targetType, Arguments constructorArguments)
        {
            var mock = Create.Fake(targetType, options =>
                _ = targetType.IsInterface ? options :
                options.WithArgumentsForConstructor(constructorArguments.Flatten()));

            Kernel.Register(Component.For(targetType)
                .Instance(mock)
                .OverridesExistingRegistration()
            );

            return mock;
        }
    }
}
