using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Tethos.NSubstitute
{
    /// <summary>
    /// <see cref="Tethos"/> auto-mocking system using <see cref="NSubstitute"/> to inject mocks.
    /// </summary>
    public class AutoMockingTest : BaseAutoMockingTest<AutoNSubstituteContainer>
    {
        /// <inheritdoc />
        public override void Install(IWindsorContainer container, IConfigurationStore store)
        {
            AutoResolver = new AutoNSubstituteResolver(container.Kernel);

            container.Kernel.Resolver.AddSubResolver(
                AutoResolver);

            base.Install(container, store);
        }
    }
}
