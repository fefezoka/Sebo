using Microsoft.EntityFrameworkCore;
using SEBO.API.Domain.Entities.ProductAggregate;

namespace SEBO.API.Data.Mapping.ProductAggregate
{
    public class ItemMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .ToTable("TB_ITEM");

            modelBuilder.Entity<Item>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Item>()
                .Property(x => x.Id)
                .HasColumnName("ID");

            modelBuilder.Entity<Item>()
                .Property(x => x.Price)
                .HasColumnName("PRICE");

            modelBuilder.Entity<Item>()
                .Property(x => x.isOutOfStock)
                .HasColumnName("IS_OUT_OF_STOCK");

            modelBuilder.Entity<Item>()
                .Property(x => x.Active)
                .HasColumnName("ACTIVE");

            modelBuilder.Entity<Item>()
               .Property(x => x.Author)
               .HasColumnName("AUTHOR");

            modelBuilder.Entity<Item>()
              .Property(x => x.CategoryId)
              .HasColumnName("CATEGORY_ID");

            modelBuilder.Entity<Item>()
              .Property(x => x.Description)
              .HasColumnName("DESCRIPTION");

            modelBuilder.Entity<Item>()
             .Property(x => x.Title)
             .HasColumnName("TITLE");

            modelBuilder.Entity<Item>()
             .Property(x => x.SellerId)
             .HasColumnName("SELLER_ID");

            modelBuilder.Entity<Item>()
                .Property(x => x.CreateDate)
                .HasColumnName("CREATE_DATE");

            modelBuilder.Entity<Item>()
                .Property(x => x.AlterDate)
                .HasColumnName("ALTER_DATE");

            modelBuilder.Entity<Item>()
                .HasOne(x => x.Seller)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.SellerId);

            modelBuilder.Entity<Item>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
