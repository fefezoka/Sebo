using Microsoft.AspNetCore.Identity;
using SEBO.API.Domain.Entities.ProductAggregate;

namespace SEBO.API.Domain.Entities.IdentityAggregate
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? AlterDate { get; set; }
        public bool Active { get; set; } = true;
        public virtual ICollection<Item> Items { get; set; }
        public ICollection<Transaction> SoldTransactions { get; set; }
        public ICollection<Transaction> BoughtTransactions { get; set; }
    }
}
