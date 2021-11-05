using Castle.MicroKernel;
using Castle.MicroKernel.Context;
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
        public override object MapToTarget(Type targetType, CreationContext context)
        {
            var mockType = typeof(Mock<>).MakeGenericType(targetType);
            var mock = Kernel.Resolve(mockType) as Mock;
            
            return mock?.Object;
        }
    }
}
