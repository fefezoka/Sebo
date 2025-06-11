using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SEBO.Ioc.Modules;

namespace SEBO.Ioc
{
    public class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            IdentityModule.AddAuthentication(services, configuration);
            RepositoryModule.InjectDependencies(services);
            ServiceModule.InjectDependencies(services);
            MiddlewareModule.InjectDependencies(services);
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }
    }
}
