using Microsoft.EntityFrameworkCore;
using SEBO.API.Domain.Entities.ProductAggregate;

namespace SEBO.API.Data.Mapping.ProductAggregate
{
    public class TransactionMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .ToTable("TB_TRANSACTION");

            modelBuilder.Entity<Transaction>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Transaction>()
                .Property(x => x.Id)
                .HasColumnName("ID");

            modelBuilder.Entity<Transaction>()
                .Property(x => x.ItemId)
                .HasColumnName("ITEM_ID");

            modelBuilder.Entity<Transaction>()
                .Property(x => x.TransactionPrice)
                .HasColumnName("TRANSACTION_PRICE");

            modelBuilder.Entity<Transaction>()
                .Property(x => x.Active)
                .HasColumnName("ACTIVE");

            modelBuilder.Entity<Transaction>()
                .Property(x => x.BuyerId)
                .HasColumnName("BUYER_ID");

            modelBuilder.Entity<Transaction>()
                .Property(x => x.SellerId)
                .HasColumnName("SELLER_ID");

            modelBuilder.Entity<Transaction>()
                .Property(x => x.TransactionDate)
                .HasColumnName("TRANSACTION_DATE");

            modelBuilder.Entity<Transaction>()
               .Property(x => x.CreateDate)
               .HasColumnName("CREATE_DATE");

            modelBuilder.Entity<Transaction>()
                .Property(x => x.AlterDate)
                .HasColumnName("ALTER_DATE");

            modelBuilder.Entity<Transaction>()
                .HasOne(x => x.Seller)
                .WithMany(x => x.SoldTransactions)
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(x => x.Buyer)
                .WithMany(x => x.BoughtTransactions)
                .HasForeignKey(x => x.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(x => x.Item)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
