using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Reflection;

namespace Tethos
{
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
        public T Container { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BaseAutoMockingTest()
        {
            Assemblies = Assembly.GetAssembly(GetType()).GetDependencies();
            Container = (T)new T().Install(this);
        }

        /// <inheritdoc />
        public virtual void Install(IWindsorContainer container, IConfigurationStore store)
        {
            foreach (var assembly in Assemblies)
            {
                container.Register(Classes.FromAssembly(assembly)
                    .Pick()
                    .WithServiceBase()
                    .WithServiceAllInterfaces()
                    .WithServiceSelf()
                    .LifestyleTransient()
                );
            }
        }

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
        protected virtual void Dispose(bool disposing)
        {
            Container?.Dispose();
        }

        /// <summary>
        /// Releases automocking resolver from <see cref="WindsorContainer"/>.
        /// Restoring container to normal function without auto-mocking.
        /// </summary>
        public void Clean()
        {
            Container
                .Kernel
                .Resolver.RemoveSubResolver(AutoResolver);
        }
    }
}
