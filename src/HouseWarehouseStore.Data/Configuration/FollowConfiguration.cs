using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class FollowConfiguration : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> entity)
        {
            entity.ToTable("Follows");

            entity.Property(e => e.FollowId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("FollowId");

            entity.Property(e => e.FollowLink).HasMaxLength(500);

            entity.Property(e => e.Icon).HasMaxLength(100);
        }
    }
}