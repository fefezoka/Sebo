using SEBO.Domain.Entities.ProductAggregate;
using SEBO.Domain.Interface.Repository.Base;

namespace SEBO.Domain.Interface.Repository.ProductAggregate
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetByUserId(int id);
    }
}
