using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class ProductCategoryConguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> entity)
        {
            entity.ToTable("ProductCategories");

            entity.HasKey(e => e.ProductCategorieId);

            entity.Property(e => e.ProductCategorieId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("ProductCategorieID");

            entity.Property(e => e.CoverImage).HasMaxLength(500);

            entity.Property(e => e.Image).HasMaxLength(500);

            entity.Property(e => e.Icon).HasMaxLength(500);

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.Property(e => e.ParentId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.TitleMeta).HasMaxLength(100);

            entity.Property(e => e.Url).HasMaxLength(500);
        }
    }
}