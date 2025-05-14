using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SEBO.API.Data.Mapping.IdentityAggregate;
using SEBO.API.Data.Mapping.ProductAggregate;
using SEBO.API.Domain.Entities.IdentityAggregate;
using SEBO.API.Domain.Entities.ProductAggregate;

namespace SEBO.API.Data
{
    public class SEBOContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public SEBOContext(DbContextOptions<SEBOContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplicationUserMap.Map(modelBuilder);
            CategoryMap.Map(modelBuilder);
            TransactionMap.Map(modelBuilder);
            ItemMap.Map(modelBuilder);
        }
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
    }
}
