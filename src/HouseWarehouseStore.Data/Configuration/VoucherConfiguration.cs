using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> entity)
        {
            entity.ToTable("Voucher");

            entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(6);

            entity.Property(e => e.Name).IsRequired();

            entity.Property(e => e.PriceDown).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.PriceUp).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.ReductionMax).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");
        }
    }
}