namespace Tethos.Tests.SUT
{
    using System;
    using Castle.MicroKernel;
    using Moq;

    internal class MapToMockArgs
    {
        public Type TargetType { get; set; }

        public object TargetObject { get; set; }

        public Arguments ConstructorArguments { get; set; }
    }

    internal class MockedAutoResolver : BaseAutoResolver
    {
        public MockedAutoResolver(IKernel kernel)
            : base(kernel)
        {
        }

        public override object MapToMock(Type targetType, object targetObject, Arguments constructorArguments) =>
            new MapToMockArgs
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
