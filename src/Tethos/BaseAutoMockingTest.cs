using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Linq;
using System.Reflection;

namespace Tethos
{
    /// <summary>
    /// Base for <see cref="Tethos"/> auto-mocking system.
    /// </summary>
    /// <typeparam name="T">Container instance type derived from <see cref="IAutoMockingContainer"/>.</typeparam>
    public abstract class BaseAutoMockingTest<T> : IWindsorInstaller, IDisposable
        where T : IAutoMockingContainer, new()
    {
        /// <summary>
        /// Auto-mocking container
        /// </summary>
        internal AutoResolver AutoResolver { get; set; }

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
            Assemblies = Assembly.GetAssembly(GetType()).GetDependencies();
            Container = (T)new T().Install(this);
        }

        /// <inheritdoc />
        public virtual void Install(IWindsorContainer container, IConfigurationStore store) =>
            container.Register(
                Assemblies.Select(assembly =>
                    Classes.FromAssembly(assembly)
                        .Pick()
                        .WithServiceBase()
                        .WithServiceAllInterfaces()
                        .WithServiceSelf()
                        .LifestyleTransient())
                .ToArray());

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes <see cref="IWindsorContainer"/> current instance.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) => Container?.Dispose();

        /// <summary>
        /// Releases automocking resolver from <see cref="WindsorContainer"/>.
        /// Restoring container to normal function without auto-mocking.
        /// </summary>
        public void Clean() =>
            Container
                .Kernel
                .Resolver.RemoveSubResolver(AutoResolver);
    }
}
