namespace Tethos.Extensions;

using System;
using Castle.MicroKernel.Registration;

internal static class WindsorExtensions
{
    internal static FromAssemblyDescriptor IncludeNonPublicTypes(
        this FromAssemblyDescriptor descriptor,
        AutoMockingConfiguration configuration) =>
     configuration switch
     {
         AutoMockingConfiguration { IncludeNonPublicTypes: true } => descriptor.IncludeNonPublicTypes(),
         _ => descriptor,
     };

    internal static ComponentRegistration<T> OverridesExistingRegistration<T>(
        this ComponentRegistration<T> componentRegistration)
        where T : class => componentRegistration?.Named($"{Guid.NewGuid()}").IsDefault();
}
