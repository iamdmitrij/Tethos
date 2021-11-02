namespace Tethos.NSubstitute
{
    using System;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Registration;
    using global::NSubstitute;

    /// <summary>
    /// <see cref="AutoResolver"/> tailored for <see cref="NSubstitute"/> mocking systems.
    /// </summary>
    public class AutoNSubstituteResolver : AutoResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoNSubstituteResolver"/> class.
        /// </summary>
        /// <param name="kernel"><see cref="Castle.Windsor"/> kernel setup.</param>
        public AutoNSubstituteResolver(IKernel kernel)
            : base(kernel)
        {
        }

        /// <inheritdoc />
        public override object MapToTarget(Type targetType)
        {
            // TODO: Pass constructor arguments received from Castle Container.Resolve(params)
            var mock = Substitute.For(new Type[] { targetType }, new object[] { });

            this.Kernel.Register(Component.For(targetType)
                .Instance(mock)
                .OverridesExistingRegistration());

            return mock;
        }
    }
}
