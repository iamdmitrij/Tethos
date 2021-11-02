namespace Tethos
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    /// <summary>
    /// Base for <see cref="Tethos"/> auto-mocking system.
    /// </summary>
    public abstract class BaseAutoMockingTest<T> : IWindsorInstaller, IDisposable
        where T : IAutoMockingContainer, new()
    {
        /// <summary>
        /// Auto-mocking container
        /// </summary>
        protected AutoResolver AutoResolver { get; set; }

        /// <summary>
        /// Entry assembly from which sub-dependencies are loaded into <see cref="Castle.Windsor"/> IoC.
        /// </summary>
        public virtual Assembly[] Assemblies { get; }

        /// <summary>
        /// <see cref="Castle.Windsor"/> container dependency.
        /// </summary>
        public T Container { get; internal set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected BaseAutoMockingTest()
        {
            this.Assemblies = Assembly.GetAssembly(this.GetType()).GetDependencies();
            this.Container = (T)new T().Install(this);
        }

        /// <inheritdoc />
        public virtual void Install(IWindsorContainer container, IConfigurationStore store) =>
            container.Register(
                this.Assemblies.Select(assembly =>
                    Classes.FromAssembly(assembly)
                        .Pick()
                        .WithServiceBase()
                        .WithServiceAllInterfaces()
                        .WithServiceSelf()
                        .LifestyleTransient()
                )
                .ToArray()
            );

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes <see cref="IWindsorContainer"/> current instance.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) => this.Container?.Dispose();

        /// <summary>
        /// Releases automocking resolver from <see cref="WindsorContainer"/>.
        /// Restoring container to normal function without auto-mocking.
        /// </summary>
        public void Clean() =>
            this.Container
                .Kernel
                .Resolver.RemoveSubResolver(this.AutoResolver);
    }
}
