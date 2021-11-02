namespace Tethos.Moq
{
    using System;
    using Castle.MicroKernel;
    using Moq;

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
            var mock = this.Kernel.Resolve(mockType) as Mock;
            
            return mock?.Object;
        }
    }
}
