using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using FakeItEasy;
using FakeItEasy.Sdk;
using System;
using System.Linq;

namespace Tethos.FakeItEasy
{
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
        public override object MapToTarget(object targetObject, Type targetType)
        {
            var fakedObject = Create.Fake(targetType);

            Kernel.Register(Component.For(targetType)
                .Instance(fakedObject)
                .OverridesExistingRegistration()
            );

            return fakedObject;
        }
    }
}
