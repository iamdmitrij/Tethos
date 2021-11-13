﻿using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using NSubstitute;
using System;

namespace Tethos.NSubstitute
{
    /// <inheritdoc />
    internal class AutoNSubstituteResolver : AutoResolver
    {
        /// <inheritdoc />
        public AutoNSubstituteResolver(IKernel kernel) : base(kernel)
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
            var arguments = targetType.IsInterface ? Array.Empty<object>() : constructorArguments.Flatten();
            var mock = Substitute.For(new Type[] { targetType }, arguments);

            Kernel.Register(Component.For(targetType)
                .Instance(mock)
                .OverridesExistingRegistration()
            );

            return mock;
        }
    }
}
