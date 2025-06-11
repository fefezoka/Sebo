using SEBO.Data.Context;
using SEBO.Data.Repository.Base;
using SEBO.Domain.Entities.ProductAggregate;
using SEBO.Domain.Interface.Repository.ProductAggregate;

namespace SEBO.Repository.ProductAggregate
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(SEBOContext context) : base(context) { }
    }
}
