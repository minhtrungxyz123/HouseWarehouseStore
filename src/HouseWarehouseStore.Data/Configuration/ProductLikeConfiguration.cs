using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class ProductLikeConfiguration : IEntityTypeConfiguration<ProductLike>
    {
        public void Configure(EntityTypeBuilder<ProductLike> entity)
        {
            entity.ToTable("ProductLikes");

            entity.HasIndex(e => e.MemberId, "IX_ProductLikes_MemberId");

            entity.HasIndex(e => e.ProductsProductId, "IX_ProductLikes_ProductsProductID");

            entity.Property(e => e.ProductLikeId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("ProductLikeID");

            entity.Property(e => e.MemberId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.ProductId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("ProductID");

            entity.Property(e => e.ProductsProductId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("ProductsProductID");
        }
    }
}