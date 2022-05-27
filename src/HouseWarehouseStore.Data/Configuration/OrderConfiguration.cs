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

            entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasDefaultValueSql("(N'')");

            entity.Property(e => e.Body).HasMaxLength(200);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'')");

            entity.Property(e => e.Fullname)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'')");

            entity.Property(e => e.Gender).HasMaxLength(10);

            entity.Property(e => e.MaDonHang).HasMaxLength(50);

            entity.Property(e => e.Mobile)
                .IsRequired()
                .HasMaxLength(11)
                .HasDefaultValueSql("(N'')");
        }
    }
}