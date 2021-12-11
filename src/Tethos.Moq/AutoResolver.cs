namespace Tethos.Moq
{
    using System;
    using System.Linq;
    using Castle.Core;
    using Castle.DynamicProxy;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;
    using Castle.MicroKernel.Registration;
    using global::Moq;
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
            DependencyModel dependency) =>
            (dependency.TargetType.IsClass && context.AdditionalArguments.Any())
            || base.CanResolve(context, contextHandlerResolver, model, dependency);

        /// <inheritdoc />
        public override object MapToMock(Type targetType, object targetObject, Arguments constructorArguments)
        {
            var mockType = typeof(Mock<>).MakeGenericType(targetType);
            var arguments = constructorArguments
                .Select(argument => argument.Value)
                .ToArray();
            var mock = Activator.CreateInstance(mockType, arguments) as Mock;
            var isPlainObject = !ProxyUtil.IsProxy(targetObject);

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
