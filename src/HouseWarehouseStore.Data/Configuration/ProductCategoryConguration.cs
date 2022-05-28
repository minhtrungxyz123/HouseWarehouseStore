using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class ProductCategoryConguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> entity)
        {
            entity.ToTable("ProductCategory");

            entity.HasKey(e => e.ProductCategorieId);

            entity.Property(e => e.ProductCategorieId).HasColumnName("ProductCategorieID");

            entity.Property(e => e.CoverImage)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(e => e.Image)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.TitleMeta).HasMaxLength(100);

            entity.Property(e => e.Url).HasMaxLength(500);
        }
    }
}