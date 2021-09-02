using Microsoft.Extensions.DependencyInjection;
using Tethos.Moq;

namespace Tethos.xUnit.Demo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAutoMoqContainer>(_ => AutoMoqContainerFactory.Create());
        }
    }
}
