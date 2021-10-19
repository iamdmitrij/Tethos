using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Tethos.NSubstitute
{
    /// <summary>
    /// TODO: Need to have better wording on this.
    /// </summary>
    public class AutoMockingTest : BaseAutoMockingTest<AutoNSubstituteContainer>
    {
        /// <inheritdoc />
        public override void Install(IWindsorContainer container, IConfigurationStore store)
        {
            AutoResolver = new AutoNSubstituteResolver(container.Kernel);

            container.Kernel.Resolver.AddSubResolver(
                AutoResolver
            );

            //container.Register(Component.For(typeof(NSubstitute<>)));

            base.Install(container, store);
        }
    }
}
