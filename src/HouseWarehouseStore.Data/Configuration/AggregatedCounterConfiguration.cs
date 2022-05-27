using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class AggregatedCounterConfiguration : IEntityTypeConfiguration<AggregatedCounter>
    {
        public void Configure(EntityTypeBuilder<AggregatedCounter> entity)
        {
            entity.ToTable("AggregatedCounter");

            entity.HasKey(e => e.Key)
                     .HasName("PK_HangFire_CounterAggregated");

            entity.ToTable("AggregatedCounter", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_AggregatedCounter_ExpireAt")
                .HasFilter("([ExpireAt] IS NOT NULL)");

            entity.Property(e => e.Key).HasMaxLength(100);

            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
        }
    }
}