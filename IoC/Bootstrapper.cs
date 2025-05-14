using Microsoft.AspNetCore.Mvc.Infrastructure;
using SEBO.API.IoC.Modules;

namespace SEBO.API.IoC
{
    public class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            IdentityModule.AddAuthentication(services, configuration);
            ServiceModule.InjectDependencies(services);
            MiddlewareModule.InjectDependencies(services);
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }
    }
}
