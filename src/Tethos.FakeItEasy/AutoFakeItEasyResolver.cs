﻿using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using FakeItEasy.Sdk;
using System;

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
        public override object MapToTarget(Type targetType, CreationContext context)
        {
            var mock = Create.Fake(targetType);

            Kernel.Register(Component.For(targetType)
                .Instance(mock)
                .OverridesExistingRegistration()
            );

            return mock;
        }
    }
}
