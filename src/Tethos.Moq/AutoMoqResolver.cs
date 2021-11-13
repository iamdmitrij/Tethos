﻿using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using Moq;
using System;
using System.Linq;

namespace Tethos.Moq
{
    /// <inheritdoc />
    internal class AutoMoqResolver : AutoResolver
    {
        /// <inheritdoc />
        public AutoMoqResolver(IKernel kernel) : base(kernel)
        {
        }

        /// <inheritdoc />
        public override bool CanResolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency
        ) => dependency.TargetType.IsClass && context.AdditionalArguments.Any() // TODO: Add coverage for default ctor
            || base.CanResolve(context, contextHandlerResolver, model, dependency);

        /// <inheritdoc />
        public override object MapToTarget(Type targetType, Arguments constructorArguments)
        {
            var mockType = typeof(Mock<>).MakeGenericType(targetType);
            var arguments = constructorArguments.Select(x => x.Value).ToArray();
            var mock = Activator.CreateInstance(mockType, arguments) as Mock;

            Kernel.Register(Component.For(mockType)
                .Instance(mock)
                .OverridesExistingRegistration()
            );

            return mock.Object;
        }
    }
}
