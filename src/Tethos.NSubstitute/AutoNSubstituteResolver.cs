using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using NSubstitute;
using System;

namespace Tethos.NSubstitute
{
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
        public override object MapToTarget(object targetObject, Type targetType)
        {
            var mock = Substitute.For(new Type[] { targetType }, new object[] { });

            Kernel.Register(Component.For(targetType)
                .Instance(mock)
                .OverridesExistingRegistration()
            );

            return mock;
        }
    }
}
