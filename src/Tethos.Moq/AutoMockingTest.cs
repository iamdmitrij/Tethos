﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Moq;

namespace Tethos.Moq
{
    /// <summary>
    /// <see cref="Tethos"/> auto-mocking system using <see cref="Moq"/> to inject mocks.
    /// </summary>
    public class AutoMockingTest : BaseAutoMockingTest<AutoMoqContainer>
    {
        /// <inheritdoc />
        public override void Install(IWindsorContainer container, IConfigurationStore store)
        {
            AutoResolver = new AutoMoqResolver(container.Kernel);

            container.Kernel.Resolver.AddSubResolver(
                AutoResolver);

            container.Register(Component.For(typeof(Mock<>)));

            base.Install(container, store);
        }
    }
}
