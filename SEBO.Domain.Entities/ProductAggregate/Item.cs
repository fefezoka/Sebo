using SEBO.Domain.Entities.Base;
using SEBO.Domain.Entities.IdentityAggregate;

namespace SEBO.Domain.Entities.ProductAggregate
{
    public class Item : BaseEntity
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool isOutOfStock { get; set; }
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
        public virtual ApplicationUser Seller { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
