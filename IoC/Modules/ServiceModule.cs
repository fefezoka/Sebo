using SEBO.API.Domain.Interface.Services;
using SEBO.API.Domain.Interface.Services.Identity;
using SEBO.API.Services;
using SEBO.API.Services.Identity;

namespace SEBO.API.IoC.Modules
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
