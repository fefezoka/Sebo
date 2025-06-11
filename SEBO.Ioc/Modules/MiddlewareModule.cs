using Microsoft.Extensions.DependencyInjection;
using SEBO.Middleware;

namespace SEBO.Ioc.Modules
{
    public static class MiddlewareModule
    {
        public static void InjectDependencies(IServiceCollection builder)
        {
            builder.AddTransient<RequestMiddleware>();
        }
    }
}