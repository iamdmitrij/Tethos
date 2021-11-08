using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using FakeItEasy.Sdk;
using System;
using System.Linq;

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
        public override bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
            => dependency.TargetType.IsClass || base.CanResolve(context, contextHandlerResolver, model, dependency);

        /// <inheritdoc />
        public override object MapToTarget(Type targetType, CreationContext context)
        {
            var arguments = context.AdditionalArguments
                .Where(argument => GetType(argument.Key) == $"{targetType}")
                .Select(argument => argument.Value)
                .ToArray();

            var mock = Create.Fake(targetType, options =>
                _ = targetType.IsInterface ? options :
                options.WithArgumentsForConstructor(arguments));

            Kernel.Register(Component.For(targetType)
                .Instance(mock)
                .OverridesExistingRegistration()
            );

            return mock;
        }

        internal string GetType(object argument) =>
            argument.ToString().Split(new string[] { "__" }, StringSplitOptions.None).FirstOrDefault();
    }
}
