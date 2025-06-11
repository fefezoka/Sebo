using Microsoft.EntityFrameworkCore;
using SEBO.Data.Context;
using SEBO.Data.Repository.Base;
using SEBO.Domain.Entities.ProductAggregate;
using SEBO.Domain.Interface.Repository.ProductAggregate;

namespace SEBO.Repository.ProductAggregate
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(SEBOContext context) : base(context) { }

        public async Task<IEnumerable<Transaction>> GetByUserId(int id) => await _dbSet.Where(x => x.SellerId == id || x.BuyerId == id).ToListAsync();
    }
}
