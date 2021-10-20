using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using FakeItEasy;
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
        public override Type DiamondType { get => typeof(Fake<>); }

        /// <inheritdoc />
        public override object MapToTarget(object targetObject, Type targetType)
        {
            var type = targetObject.GetType();

            var fakedObject = type
                .GetProperties()
                .FirstOrDefault(x => x.Name == "FakedObject")
                .GetValue(targetObject, null);

            Kernel.Register(Component.For(targetType)
                .Instance(fakedObject)
                .OverridesExistingRegistration()
            );

            return fakedObject;
        }
    }
}
