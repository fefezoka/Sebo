using SEBO.Domain.Entities.Base;

namespace SEBO.Domain.Entities.ProductAggregate
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
