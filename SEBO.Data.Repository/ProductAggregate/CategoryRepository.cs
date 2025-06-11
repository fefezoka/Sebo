using SEBO.Data.Context;
using SEBO.Data.Repository.Base;
using SEBO.Domain.Entities.ProductAggregate;
using SEBO.Domain.Interface.Repository.ProductAggregate;

namespace SEBO.Repository.ProductAggregate
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(SEBOContext context) : base(context) { }
    }
}
