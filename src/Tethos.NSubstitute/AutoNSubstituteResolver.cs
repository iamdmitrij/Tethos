namespace Tethos.NSubstitute
{
    using System;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Registration;
    using NSubstitute;

    /// <summary>
    /// <see cref="AutoResolver"/> tailored for <see cref="NSubstitute"/> mocking systems.
    /// </summary>
    public class AutoNSubstituteResolver : AutoResolver
    {
        /// <inheritdoc />
        public AutoNSubstituteResolver(IKernel kernel) : base(kernel)
        {
        }

        /// <inheritdoc />
        public override object MapToTarget(Type targetType)
        {
            // TODO: Pass constructor arguments received from Castle Container.Resolve(params)
            var mock = Substitute.For(new Type[] { targetType }, new object[] { });

            Kernel.Register(Component.For(targetType)
                .Instance(mock)
                .OverridesExistingRegistration()
            );

            return mock;
        }
    }
}
