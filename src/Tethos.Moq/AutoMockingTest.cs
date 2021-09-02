using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Moq;

namespace Tethos.Moq
{
    /// <summary>
    /// TODO: Need to have better wording on this.
    /// </summary>
    public class AutoMockingTest : BaseAutoMockingTest<AutoMoqContainer>
    {
        /// <inheritdoc />
        public override void Install(IWindsorContainer container, IConfigurationStore store)
        {
            AutoResolver = new AutoMoqResolver(container.Kernel);

            container.Kernel.Resolver.AddSubResolver(
                AutoResolver
            );

            container.Register(Component.For(typeof(Mock<>)));

            base.Install(container, store);
        }
    }
}
