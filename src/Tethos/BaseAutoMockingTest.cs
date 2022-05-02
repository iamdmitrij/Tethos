﻿namespace Tethos;

using System;
using System.IO;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Tethos.Extensions;
using Tethos.Extensions.Assembly;

/// <summary>
/// Base for <see cref="Tethos"/> auto-mocking system.
/// </summary>
/// <typeparam name="T">Container instance type derived from <see cref="IAutoMockingContainer"/>.</typeparam>
public abstract class BaseAutoMockingTest<T> : IWindsorInstaller, IDisposable
    where T : IAutoMockingContainer, new()
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseAutoMockingTest{T}"/> class.
    /// </summary>
    protected BaseAutoMockingTest()
    {
        this.Assemblies = this.GetType().GetRelatedAssemblies(this.AutoMockingConfiguration);
        this.Container = (T)new T().Install(this);
    }

    /// <summary>
    /// Gets entry assembly from which sub-dependencies are loaded into <see cref="Castle.Windsor"/> IoC.
    /// </summary>
    public virtual Assembly[] Assemblies { get; }

    /// <summary>
    /// Gets <see cref="Castle.Windsor"/> container dependency.
    /// </summary>
    public T Container { get; internal set; }

    public virtual AutoMockingConfiguration AutoMockingConfiguration => this.OnConfigurationCreated(new());

    /// <summary>
    /// Gets or sets auto-mocking container.
    /// </summary>
    internal BaseAutoResolver AutoResolver { get; set; }

    /// <inheritdoc />
    public virtual void Install(IWindsorContainer container, IConfigurationStore store)
    {
        foreach (var assembly in this.Assemblies)
        {
            var func = () => container.Register(Classes.FromAssembly(assembly)
                .IncludeNonPublicTypes(this.AutoMockingConfiguration)
                .Pick()
                .WithServiceBase()
                .WithServiceAllInterfaces()
                .WithServiceSelf()
                .LifestyleTransient());
            func.SwallowExceptions(typeof(FileNotFoundException));
        }
    }

    /// <summary>
    /// Method which lets user to configure <see cref="AutoMockingConfiguration"/>.
    /// </summary>
    /// <param name="configuration">Container configuration.</param>
    /// <returns>Final auto-mocking container configuration.</returns>
    public virtual AutoMockingConfiguration OnConfigurationCreated(AutoMockingConfiguration configuration) => configuration;

    /// <summary>
    /// Releases automocking resolver from <see cref="WindsorContainer"/>.
    /// Restoring container to normal function without auto-mocking.
    /// </summary>
    public void Clean() =>
        this.Container
            .Kernel
            .Resolver.RemoveSubResolver(this.AutoResolver);

    /// <inheritdoc />
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes <see cref="IWindsorContainer"/> current instance.
    /// </summary>
    /// <param name="disposing">Is instance disposing.</param>
    protected virtual void Dispose(bool disposing) => this.Container?.Dispose();
}
