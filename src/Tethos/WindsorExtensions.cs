using Castle.MicroKernel.Registration;
using System;

namespace Tethos
{
    internal static class WindsorExtensions
    {
        internal static ComponentRegistration<T> OverridesExistingRegistration<T>(
            this ComponentRegistration<T> componentRegistration) where T : class
            => componentRegistration?.Named($"{Guid.NewGuid()}").IsDefault();
    }
}
