using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("Products");

            entity.HasIndex(e => e.CollectionId, "IX_Products_CollectionID");

            entity.HasIndex(e => e.ProductCategorieId, "IX_Products_ProductCategorieID");

            entity.Property(e => e.ProductId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("ProductID");

            entity.Property(e => e.BarCode).HasMaxLength(50);

            entity.Property(e => e.CollectionId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("CollectionID");

            entity.Property(e => e.CreateBy).HasDefaultValueSql("(N'admin')");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Factory).HasMaxLength(501);

            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.ProductCategorieId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("ProductCategorieID");

            entity.Property(e => e.QuyCach).HasMaxLength(500);

            entity.Property(e => e.SaleOff).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.Sort).HasDefaultValueSql("((1))");

            entity.Property(e => e.TitleMeta).HasMaxLength(100);
        }
    }
}