namespace Tethos.Tests.SUT
{
    using System;
    using Castle.MicroKernel;

    internal class MockedAutoResolver : BaseAutoResolver
    {
        public MockedAutoResolver(IKernel kernel)
            : base(kernel)
        {
        }

        public override object MapToMock(Type targetType, object targetObject, Arguments constructorArguments) =>
            new MapToMockArguments
            {
                TargetType = targetType,
                TargetObject = targetObject,
                ConstructorArguments = constructorArguments,
            };
    }

    internal class ConcreteAutoResolver : BaseAutoResolver
    {
        public ConcreteAutoResolver(IKernel kernel)
            : base(kernel)
        {
        }

        public override object MapToMock(Type targetType, object targetObject, Arguments constructorArguments) => targetObject;
    }
}
