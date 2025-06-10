using Microsoft.EntityFrameworkCore;
using SEBO.API.Data;
using SEBO.API.Data.Repository.Base;
using SEBO.API.Domain.Entities.ProductAggregate;
using SEBO.API.Domain.Interface.Repository.ProductAggregate;

namespace SEBO.API.Repository.ProductAggregate
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(SEBOContext context) : base(context) { }

        public async Task<IEnumerable<Transaction>> GetByUserId(int id) => await _dbSet.Where(x => x.SellerId == id || x.BuyerId == id).ToListAsync();
    }
}
