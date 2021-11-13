using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using FakeItEasy.Creation;
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
            Action<IFakeOptions> arguments = targetType.IsInterface switch
            {
                true => options => { }
                ,
                false => options => options.WithArgumentsForConstructor(constructorArguments.Flatten()),
            };
            // TODO: Tests won't fail if next line is changed to var mock = Create.Fake(targetType);
            var mock = Create.Fake(targetType, arguments);

            Kernel.Register(Component.For(targetType)
                .Instance(mock)
                .OverridesExistingRegistration()
            );

            return mock;
        }
    }
}
