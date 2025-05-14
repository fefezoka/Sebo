using SEBO.API.Middleware;

namespace SEBO.API.IoC.Modules
{
    public static class MiddlewareModule
    {
        public static void InjectDependencies(IServiceCollection builder)
        {
            builder.AddTransient<RequestMiddleware>();
        }
    }
}