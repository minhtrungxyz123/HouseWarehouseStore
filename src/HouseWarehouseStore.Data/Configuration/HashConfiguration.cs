using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class HashConfiguration : IEntityTypeConfiguration<Hash>
    {
        public void Configure(EntityTypeBuilder<Hash> entity)
        {
            entity.ToTable("Hash");

            entity.HasKey(e => new { e.Key, e.Field })
                    .HasName("PK_HangFire_Hash");

            entity.ToTable("Hash", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Hash_ExpireAt")
                .HasFilter("([ExpireAt] IS NOT NULL)");

            entity.Property(e => e.Key).HasMaxLength(100);

            entity.Property(e => e.Field).HasMaxLength(100);
        }
    }
}