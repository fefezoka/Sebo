using Microsoft.EntityFrameworkCore;
using SEBO.API.Domain.Entities.ProductAggregate;
using System;

namespace SEBO.API.Data.Mapping.ProductAggregate
{
    public class CategoryMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Category>()
                .ToTable("TB_CATEGORY");

            modelBuilder.Entity<Category>()
                .Property(x => x.Id)
                .HasColumnName("ID");

            modelBuilder.Entity<Category>()
                .Property(x => x.Active)
                .HasColumnName("ACTIVE");

            modelBuilder.Entity<Category>()
                .Property(x => x.Description)
                .HasColumnName("DESCRIPTION");

            modelBuilder.Entity<Category>()
                .Property(x => x.Name)
                .HasColumnName("NAME");

            modelBuilder.Entity<Category>()
                .Property(x => x.CreateDate)
                .HasColumnName("CREATE_DATE");

            modelBuilder.Entity<Category>()
                .Property(x => x.AlterDate)
                .HasColumnName("ALTER_DATE");

            modelBuilder.Entity<Category>()
                .HasMany(x => x.Items)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
