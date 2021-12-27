namespace Tethos.Tests
{
    using System;
    using Castle.MicroKernel;

    internal class MapToMockArguments
    {
        public Type TargetType { get; set; }

        public object TargetObject { get; set; }

        public Arguments ConstructorArguments { get; set; }
    }
}
