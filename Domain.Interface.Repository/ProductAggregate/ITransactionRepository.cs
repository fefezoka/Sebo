using SEBO.API.Domain.Entities.ProductAggregate;
using SEBO.API.Domain.Interface.Repository.Base;

namespace SEBO.API.Domain.Interface.Repository.ProductAggregate
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetByUserId(int id);
    }
}
