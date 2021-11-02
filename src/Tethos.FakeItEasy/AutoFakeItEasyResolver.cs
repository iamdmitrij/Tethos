namespace Tethos.FakeItEasy
{
    using System;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Registration;
    using FakeItEasy.Sdk;

    /// <summary>
    /// <see cref="AutoResolver"/> tailored for <see cref="FakeItEasy"/> mocking systems.
    /// </summary>
    public class AutoFakeItEasyResolver : AutoResolver
    {
        /// <inheritdoc />
        public AutoFakeItEasyResolver(IKernel kernel) : base(kernel)
        {
        }

        /// <inheritdoc />
        public override object MapToTarget(Type targetType)
        {
            var mock = Create.Fake(targetType);

            this.Kernel.Register(Component.For(targetType)
                .Instance(mock)
                .OverridesExistingRegistration()
            );

            return mock;
        }
    }
}
