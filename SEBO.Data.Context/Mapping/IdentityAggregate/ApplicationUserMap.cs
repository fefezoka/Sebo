using Microsoft.EntityFrameworkCore;
using SEBO.Domain.Entities.IdentityAggregate;

namespace SEBO.Data.Context.Mapping.IdentityAggregate
{
    public class ApplicationUserMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("TB_USER");

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.Items)
                .WithOne(x => x.Seller)
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.SoldTransactions)
                .WithOne(x => x.Seller)
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.BoughtTransactions)
                .WithOne(x => x.Buyer)
                .HasForeignKey(x => x.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
