using Castle.MicroKernel;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Tethos.NSubstitute
{
    /// <summary>
    /// <see cref="AutoResolver"/> tailored for <see cref="Moq"/> mocking systems.
    /// </summary>
    public class AutoNSubstituteResolver : AutoResolver
    {
        /// <inheritdoc />
        public AutoNSubstituteResolver(IKernel kernel): base(kernel)
        {
        }

        /// <inheritdoc />
        public override Type DiamondType { get => typeof(NSubstitute<>); }

        /// <inheritdoc />
        public override object MapToTarget(object targetObject)
        {
            return ((NSubstitute)targetObject);
        }
    }
}
