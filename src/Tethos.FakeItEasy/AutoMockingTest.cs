using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FakeItEasy;

namespace Tethos.FakeItEasy
{
    /// <summary>
    /// TODO: Need to have better wording on this.
    /// </summary>
    public class AutoMockingTest : BaseAutoMockingTest<AutoFakeItEasyContainer>
    {
        /// <inheritdoc />
        public override void Install(IWindsorContainer container, IConfigurationStore store)
        {
            AutoResolver = new AutoFakeItEasyResolver(container.Kernel);

            container.Kernel.Resolver.AddSubResolver(
                AutoResolver
            );

            container.Register(Component.For(typeof(Fake<>)));

            base.Install(container, store);
        }
    }
}
