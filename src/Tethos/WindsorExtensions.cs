namespace Tethos
{
    using System;
    using Castle.MicroKernel.Registration;

    internal static class WindsorExtensions
    {
        internal static ComponentRegistration<T> OverridesExistingRegistration<T>(
            this ComponentRegistration<T> componentRegistration)
            where T : class
            => componentRegistration?.Named($"{Guid.NewGuid()}").IsDefault();

    }
}
