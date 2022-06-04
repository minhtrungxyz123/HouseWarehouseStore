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

            entity.Property(e => e.RecordId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("RecordID");

            entity.Property(e => e.CartId).HasColumnName("CartID");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.ProductId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("ProductID");


        }
    }
}