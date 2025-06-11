using Microsoft.Extensions.DependencyInjection;
using SEBO.Domain.Interface.Repository.IdentityAggregate;
using SEBO.Domain.Interface.Repository.ProductAggregate;
using SEBO.Repository.IdentityAggregate;
using SEBO.Repository.ProductAggregate;

namespace SEBO.Ioc.Modules
{
    public class RepositoryModule
    {
        public static void InjectDependencies(IServiceCollection builder)
        {
            builder.AddTransient<ICategoryRepository, CategoryRepository>();
            builder.AddTransient<IItemRepository, ItemRepository>();
            builder.AddTransient<IUserRepository, UserRepository>();
            builder.AddTransient<ITransactionRepository, TransactionRepository>();
        }
    }
}
