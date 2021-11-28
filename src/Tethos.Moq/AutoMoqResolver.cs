﻿namespace Tethos.Moq
{
    using System;
    using System.Linq;
    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;
    using Castle.MicroKernel.Registration;
    using global::Moq;
    using Tethos.Extensions;

    /// <inheritdoc />
    internal class AutoMoqResolver : AutoResolver
    {
        /// <inheritdoc cref="AutoResolver" />
        public AutoMoqResolver(IKernel kernel)
            : base(kernel)
        {
        }

        /// <inheritdoc />
        public override bool CanResolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency) =>

            // TODO: Add coverage for default ctor
            (dependency.TargetType.IsClass && context.AdditionalArguments.Any())
            || base.CanResolve(context, contextHandlerResolver, model, dependency);

        /// <inheritdoc />
        public override object MapToMock(Type targetType, object targetObject, Arguments constructorArguments)
        {
            var mockType = typeof(Mock<>).MakeGenericType(targetType);
            var arguments = constructorArguments.Select(argument => argument.Value).ToArray();
            var mock = Activator.CreateInstance(mockType, arguments) as Mock;
            var getMock = () => Mock.Get(targetObject);
            var isPlainObject = getMock.Throws(typeof(ArgumentException));

            if (isPlainObject)
            {
                this.Kernel.Register(Component.For(mockType)
                    .Instance(mock)
                    .OverridesExistingRegistration());

                this.Kernel.Register(Component.For(targetType)
                  .Instance(mock.Object)
                  .OverridesExistingRegistration());
            }

            return mock.Object;
        }
    }
}
