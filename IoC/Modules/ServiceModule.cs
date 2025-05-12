using SEBO.API.Repository.IdentityAggregate;
using SEBO.API.Repository.ProductAggregate;
using SEBO.API.Services.AppServices.IdentityService;
using SEBO.API.Services;

namespace SEBO.API.IoC.Modules
{
    public class ServiceModule
    {
        public static void InjectDependencies(IServiceCollection builder)
        {
            builder.AddScoped<CategoryRepository>();
            builder.AddScoped<ItemRepository>();
            builder.AddScoped<UserRepository>();
            builder.AddScoped<TransactionRepository>();
            builder.AddScoped<CategoryService>();
            builder.AddScoped<ItemService>();
            builder.AddScoped<UserService>();
            builder.AddScoped<TransactionService>();
            builder.AddScoped<AuthenticationService>();
            builder.AddScoped<TokenService>();
        }
    }
}
