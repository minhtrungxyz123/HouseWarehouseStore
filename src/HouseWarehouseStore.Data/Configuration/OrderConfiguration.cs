using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entity)
        {
            entity.ToTable("Order");

            entity.Property(e => e.Id)
                   .HasMaxLength(36)
                   .IsUnicode(false);

            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasDefaultValueSql("(N'')");

            entity.Property(e => e.Body).HasMaxLength(200);

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'')");

            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'')");

            entity.Property(e => e.Gender).HasMaxLength(10);

            entity.Property(e => e.MaDonHang).HasMaxLength(50);

            entity.Property(e => e.Mobile)
                .HasMaxLength(11)
                .HasDefaultValueSql("(N'')");

            entity.Property(e => e.OrderMemberId)
                .HasMaxLength(36)
                .IsUnicode(false);
        }
    }
}