using SEBO.API.Domain.Entities.Base;

namespace SEBO.API.Domain.Entities.ProductAggregate
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
