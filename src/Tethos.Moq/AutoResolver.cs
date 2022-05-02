namespace Tethos.Moq;

using System;
using System.Linq;
using Castle.Core;
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
    public override object MapToMock(MockMapping argument)
    {
        var isPlainObject = !typeof(IMocked).IsAssignableFrom(argument.TargetObject?.GetType());

        if (isPlainObject)
        {
            var mockType = typeof(Mock<>).MakeGenericType(argument.TargetType);
            var arguments = argument.ConstructorArguments
                ?.Select(argument => argument.Value)
                .ToArray();
            var mock = Activator.CreateInstance(mockType, arguments) as Mock;

            this.Kernel.Register(Component.For(mockType)
                .Instance(mock)
                .OverridesExistingRegistration());

            this.Kernel.Register(Component.For(argument.TargetType)
              .Instance(mock.Object)
              .OverridesExistingRegistration());

            return mock.Object;
        }

        return argument.TargetObject;
    }
}
