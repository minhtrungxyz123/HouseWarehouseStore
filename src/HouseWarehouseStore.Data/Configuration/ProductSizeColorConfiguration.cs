using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class ProductSizeColorConfiguration : IEntityTypeConfiguration<ProductSizeColor>
    {
        public void Configure(EntityTypeBuilder<ProductSizeColor> entity)
        {
            entity.ToTable("ProductSizeColors");

            entity.HasIndex(e => e.ColorId, "IX_ProductSizeColors_ColorID");

            entity.HasIndex(e => e.ProductsProductId, "IX_ProductSizeColors_ProductsProductID");

            entity.HasIndex(e => e.SizeId, "IX_ProductSizeColors_SizeID");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.ColorId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("ColorID");

            entity.Property(e => e.ProductId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("ProductID");

            entity.Property(e => e.ProductsProductId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("ProductsProductID");

            entity.Property(e => e.SizeId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("SizeID");
        }
    }
}