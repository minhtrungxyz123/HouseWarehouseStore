using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> entity)
        {
            entity.ToTable("Cart");

            entity.HasKey(e => e.RecordId);

            entity.HasIndex(e => e.ProductId, "IX_Carts_ProductID");

            entity.Property(e => e.RecordId).HasColumnName("RecordID");

            entity.Property(e => e.CartId)
                .IsRequired()
                .HasColumnName("CartID");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId);
        }
    }
}