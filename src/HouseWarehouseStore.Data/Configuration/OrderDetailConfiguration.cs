using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> entity)
        {
            entity.ToTable("OrderDetail");

            entity.HasKey(e => new { e.OrderId, e.ProductId, e.Size, e.Color });

            entity.HasIndex(e => e.ProductId, "IX_OrderDetails_ProductId");

            entity.Property(e => e.OrderId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("((0))");

            entity.Property(e => e.ProductId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
        }
    }
}