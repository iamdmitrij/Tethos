using Castle.MicroKernel;
using Moq;
using System;

namespace Tethos.Tests.SUT
{
    internal class ConcreteAutoResolver : AutoResolver
    {
        public ConcreteAutoResolver(IKernel kernel) : base(kernel)
        {
        }

        public override object MapToTarget(Type targetType, object[] constructorArguments) => new Mock<object>();
    }
}
