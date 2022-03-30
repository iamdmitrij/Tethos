namespace Tethos.Moq
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using global::Moq;

    /// <summary>
    /// <see cref="Tethos"/> auto-mocking system using <see cref="Moq"/> to inject mocks.
    /// </summary>
    public class AutoMockingTest : BaseAutoMockingTest<AutoMockingContainer>
    {
        internal IRegistration DiamondTypeComponent { get; } = Component.For(typeof(Mock<>));

        /// <inheritdoc />
        public override void Install(IWindsorContainer container, IConfigurationStore store)
        {
            this.AutoResolver = new AutoResolver(container.Kernel);

            container.Kernel.Resolver.AddSubResolver(
                this.AutoResolver);

            container.Register(this.DiamondTypeComponent);

            base.Install(container, store);
        }
    }
}
