using SEBO.API.Domain.Interface.Repository.IdentityAggregate;
using SEBO.API.Domain.Interface.Repository.ProductAggregate;
using SEBO.API.Repository.IdentityAggregate;
using SEBO.API.Repository.ProductAggregate;

namespace SEBO.API.IoC.Modules
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
