using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class ProductSizeColorConfiguration : IEntityTypeConfiguration<ProductSizeColor>
    {
        public void Configure(EntityTypeBuilder<ProductSizeColor> entity)
        {
            entity.ToTable("ProductSizeColor");

            entity.HasIndex(e => e.ColorId, "IX_ProductSizeColors_ColorID");

            entity.HasIndex(e => e.ProductsProductId, "IX_ProductSizeColors_ProductsProductID");

            entity.HasIndex(e => e.SizeId, "IX_ProductSizeColors_SizeID");

            entity.Property(e => e.ColorId).HasColumnName("ColorID");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.Property(e => e.ProductsProductId).HasColumnName("ProductsProductID");

            entity.Property(e => e.SizeId).HasColumnName("SizeID");

            entity.HasOne(d => d.Color)
                .WithMany(p => p.ProductSizeColors)
                .HasForeignKey(d => d.ColorId);

            entity.HasOne(d => d.ProductsProduct)
                .WithMany(p => p.ProductSizeColors)
                .HasForeignKey(d => d.ProductsProductId);

            entity.HasOne(d => d.Size)
                .WithMany(p => p.ProductSizeColors)
                .HasForeignKey(d => d.SizeId);
        }
    }
}