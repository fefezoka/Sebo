using Microsoft.Extensions.DependencyInjection;
using SEBO.Domain.Interface.Services;
using SEBO.Domain.Interface.Services.Identity;
using SEBO.Services;
using SEBO.Services.Identity;

namespace SEBO.Ioc.Modules
{
    public class ServiceModule
    {
        public static void InjectDependencies(IServiceCollection builder)
        {
            builder.AddTransient<ICategoryService, CategoryService>();
            builder.AddTransient<IItemService, ItemService>();
            builder.AddTransient<IUserService, UserService>();
            builder.AddTransient<ITransactionService, TransactionService>();
            builder.AddTransient<IAuthenticationService, AuthenticationService>();
            builder.AddTransient<ITokenService, TokenService>();
        }
    }
}
