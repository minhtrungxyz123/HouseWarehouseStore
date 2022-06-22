using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class UserVoucherConfiguration : IEntityTypeConfiguration<UserVoucher>
    {
        public void Configure(EntityTypeBuilder<UserVoucher> entity)
        {
            entity.ToTable("UserVouchers");

            entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("id");

            entity.Property(e => e.Code).HasMaxLength(6);

            entity.Property(e => e.MaDonHang).HasMaxLength(50);

            entity.Property(e => e.SumHd)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("SumHD");
        }
    }
}