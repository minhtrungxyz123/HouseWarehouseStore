using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class CounterConfiguration : IEntityTypeConfiguration<Counter>
    {
        public void Configure(EntityTypeBuilder<Counter> entity)
        {
            entity.ToTable("Counter");

            entity.HasNoKey();

            entity.ToTable("Counter", "HangFire");

            entity.HasIndex(e => e.Key, "CX_HangFire_Counter")
                .IsClustered();

            entity.Property(e => e.ExpireAt).HasColumnType("datetime");

            entity.Property(e => e.Key).HasMaxLength(100);
        }
    }
}