using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Moq;
using System;

namespace Tethos.Tests.SUT
{
    internal class ConcreteAutoResolver : AutoResolver
    {
        public ConcreteAutoResolver(IKernel kernel) : base(kernel)
        {
        }

        public override object MapToTarget(Type targetType, CreationContext context) => new Mock<object>();
    }
}
