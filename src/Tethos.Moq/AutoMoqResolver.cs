using Castle.MicroKernel;
using Moq;
using System;

namespace Tethos.Moq
{
    /// <summary>
    /// <see cref="AutoResolver"/> tailored for <see cref="Moq"/> mocking systems.
    /// </summary>
    public class AutoMoqResolver : AutoResolver
    {
        /// <inheritdoc />
        public AutoMoqResolver(IKernel kernel): base(kernel)
        {
        }

        /// <inheritdoc />
        public override Type DiamondType { get => typeof(Mock<>); }

        /// <inheritdoc />
        public override object MapToTarget(object targetObject, Type targetType)
        {
            // TODO: Move creation of Moq mock instance to here instead of base lib
            return ((Mock)targetObject).Object;
        }
    }
}
