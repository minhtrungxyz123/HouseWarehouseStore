using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class ReviewProductConfiguration : IEntityTypeConfiguration<ReviewProduct>
    {
        public void Configure(EntityTypeBuilder<ReviewProduct> entity)
        {
            entity.ToTable("ReviewProduct");

            entity.HasIndex(e => e.ProductId, "IX_ReviewProducts_ProductId");

            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ReviewProducts)
                .HasForeignKey(d => d.ProductId);
        }
    }
}