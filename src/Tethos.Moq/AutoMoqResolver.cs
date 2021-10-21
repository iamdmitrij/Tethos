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
        public override object MapToTarget(Type targetType)
        {
            var mockType = typeof(Mock<>).MakeGenericType(targetType);
            var targetObject = Kernel.Resolve(mockType);

            return ((Mock)targetObject).Object;
        }
    }
}
