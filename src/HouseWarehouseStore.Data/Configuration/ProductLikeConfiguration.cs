using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class ProductLikeConfiguration : IEntityTypeConfiguration<ProductLike>
    {
        public void Configure(EntityTypeBuilder<ProductLike> entity)
        {
            entity.ToTable("ProductLike");

            entity.HasIndex(e => e.MemberId, "IX_ProductLikes_MemberId");

            entity.HasIndex(e => e.ProductsProductId, "IX_ProductLikes_ProductsProductID");

            entity.Property(e => e.ProductLikeId).HasColumnName("ProductLikeID");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.Property(e => e.ProductsProductId).HasColumnName("ProductsProductID");

            entity.HasOne(d => d.Member)
                .WithMany(p => p.ProductLikes)
                .HasForeignKey(d => d.MemberId);

            entity.HasOne(d => d.ProductsProduct)
                .WithMany(p => p.ProductLikes)
                .HasForeignKey(d => d.ProductsProductId);
        }
    }
}