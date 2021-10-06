using Microsoft.Extensions.DependencyInjection;
using Tethos.Moq;

namespace Tethos.xUnit.Demo
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(_ => AutoMoqContainerFactory.Create());
        }
    }
}
