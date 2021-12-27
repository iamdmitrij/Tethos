namespace Tethos.Tests.SUT
{
    using System;
    using Castle.MicroKernel;

    internal class AutoResolver : BaseAutoResolver
    {
        public AutoResolver(IKernel kernel)
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
}
