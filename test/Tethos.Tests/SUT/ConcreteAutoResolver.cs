namespace Tethos.Tests.SUT
{
    using System;
    using Castle.MicroKernel;
    using Moq;

    internal class ConcreteAutoResolver : AutoResolver
    {
        public ConcreteAutoResolver(IKernel kernel)
            : base(kernel)
        {
        }

        public override object MapToMock(Type targetType, object targetObject, Arguments constructorArguments) => Mock.Of<object>();
    }
}
