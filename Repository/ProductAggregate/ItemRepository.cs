using SEBO.API.Data;
using SEBO.API.Data.Repository.Base;
using SEBO.API.Domain.Entities.ProductAggregate;
using SEBO.API.Domain.Interface.Repository.ProductAggregate;

namespace SEBO.API.Repository.ProductAggregate
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(SEBOContext context) : base(context) { }
    }
}
