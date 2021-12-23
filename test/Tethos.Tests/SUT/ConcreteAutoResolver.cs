namespace Tethos.Tests.SUT
{
    using System;
    using Castle.MicroKernel;

    internal class ConcreteAutoResolver : BaseAutoResolver
    {
        public ConcreteAutoResolver(IKernel kernel)
            : base(kernel)
        {
        }

        public override object MapToMock(Type targetType, object targetObject, Arguments constructorArguments) => targetObject;
    }
}
