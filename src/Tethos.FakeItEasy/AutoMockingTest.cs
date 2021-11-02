﻿namespace Tethos.FakeItEasy
{
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    /// <summary>
    /// <see cref="Tethos"/> auto-mocking system using <see cref="FakeItEasy"/> to inject mocks.
    /// </summary>
    public class AutoMockingTest : BaseAutoMockingTest<AutoFakeItEasyContainer>
    {
        /// <inheritdoc />
        public override void Install(IWindsorContainer container, IConfigurationStore store)
        {
            this.AutoResolver = new AutoFakeItEasyResolver(container.Kernel);

            container.Kernel.Resolver.AddSubResolver(
                this.AutoResolver);

            base.Install(container, store);
        }
    }
}
