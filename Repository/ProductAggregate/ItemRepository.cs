using SEBO.Data.Repository.Base;
using SEBO.API.Data;
using SEBO.API.Domain.Entities.ProductAggregate;

namespace SEBO.API.Repository.ProductAggregate
{
    public class ItemRepository : BaseRepository<Item>
    {
        public ItemRepository(SEBOContext context) : base(context) { }
    }
}
