namespace Tethos.Xunit.Demo
{
    using Microsoft.Extensions.DependencyInjection;
    using Tethos.NSubstitute;

    public class Startup
    {
#pragma warning disable CA1822 // Mark members as static
        public void ConfigureServices(IServiceCollection services)
#pragma warning restore CA1822 // Mark members as static
        {
            services.AddScoped(_ => AutoMockingContainerFactory.Create());
        }
    }
}
