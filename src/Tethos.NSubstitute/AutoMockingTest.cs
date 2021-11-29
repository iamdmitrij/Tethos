namespace Tethos.NSubstitute
{
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    /// <summary>
    /// <see cref="Tethos"/> auto-mocking system using <see cref="NSubstitute"/> to inject mocks.
    /// </summary>
    public class AutoMockingTest : BaseAutoMockingTest<AutoMockingContainer>
    {
        /// <inheritdoc />
        public override void Install(IWindsorContainer container, IConfigurationStore store)
        {
            this.AutoResolver = new AutoResolver(container.Kernel);

            container.Kernel.Resolver.AddSubResolver(
                this.AutoResolver);

            base.Install(container, store);
        }
    }
}
