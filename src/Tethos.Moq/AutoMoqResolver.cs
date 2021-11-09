using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using Moq;
using System;
using System.Linq;

namespace Tethos.Moq
{
    /// <summary>
    /// <see cref="AutoResolver"/> tailored for <see cref="Moq"/> mocking systems.
    /// </summary>
    public class AutoMoqResolver : AutoResolver
    {
        /// <inheritdoc />
        public AutoMoqResolver(IKernel kernel) : base(kernel)
        {
        }

        /// <inheritdoc />
        public override bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency) =>
            dependency.TargetType.IsClass && context.AdditionalArguments.Any()
            || base.CanResolve(context, contextHandlerResolver, model, dependency);

        /// <inheritdoc />
        public override object MapToTarget(Type targetType, Arguments constructorArguments)
        {
            var mockType = typeof(Mock<>).MakeGenericType(targetType);
            var args = constructorArguments.Select(x => x.Value).ToArray();
            var mock = Activator.CreateInstance(mockType, args) as Mock;

            Kernel.Register(Component.For(mockType)
                //.LifestyleTransient() // TODO: This will be relevant for https://github.com/iamdmitrij/Tethos/issues/46
                .Instance(mock)
            );

            return mock.Object;
        }
    }
}
