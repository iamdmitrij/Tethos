namespace Tethos.Tests.SUT
{
    using Castle.MicroKernel;
    using Moq;
    using System;

    internal class ConcreteAutoResolver : AutoResolver
    {
        public ConcreteAutoResolver(IKernel kernel)
            : base(kernel)
        {
        }

        public override object MapToTarget(Type targetType, Arguments constructorArguments) => new Mock<object>();
    }
}
