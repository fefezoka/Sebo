using MKW.Data.Repository.Base;
using SEBO.API.Data;
using SEBO.API.Domain.Entities.ProductAggregate;

namespace SEBO.API.Repository.ProductAggregate
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(SEBOContext context) : base(context) { }
    }
}
