namespace Tethos.Moq
{
    using System;
    using System.Linq;
    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;
    using Castle.MicroKernel.Handlers;
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
        public override object MapToTarget(Type targetType, Arguments constructorArguments)
        {
            var mockType = typeof(Mock<>).MakeGenericType(targetType);
            var arguments = constructorArguments.Select(x => x.Value).ToArray();
            var mock = Activator.CreateInstance(mockType, arguments) as Mock;
            var func = () => this.Kernel.Resolve(targetType);
            var currentObject = func.SwallowExceptions(typeof(ComponentNotFoundException), typeof(HandlerException));
            var isMock = false;
            try
            {
                _ = Mock.Get(currentObject);
                isMock = true;
            }
            catch (ArgumentException)
            {
                isMock = false;
            }

            if (!isMock)
            {
                this.Kernel.Register(Component.For(mockType)
                    .Instance(mock)
                    .OverridesExistingRegistration());
            }

            return mock.Object;
        }
    }
}
