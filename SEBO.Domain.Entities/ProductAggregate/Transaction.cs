using SEBO.Domain.Entities.Base;
using SEBO.Domain.Entities.IdentityAggregate;

namespace SEBO.Domain.Entities.ProductAggregate
{
    public class Transaction : BaseEntity
    {
        public int BuyerId { get; set; }
        public int ItemId { get; set; }
        public int SellerId { get; set; }
        public double TransactionPrice { get; set; }
        public DateTime TransactionDate { get; set; }
        public virtual ApplicationUser Buyer { get; set; }
        public virtual Item Item { get; set; }
        public virtual ApplicationUser Seller { get; set; }
    }
}
