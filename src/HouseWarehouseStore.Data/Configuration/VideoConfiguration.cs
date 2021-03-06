using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class VideoConfiguration : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> entity)
        {
            entity.ToTable("Video");

            entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false);

            entity.Property(e => e.Image).HasMaxLength(500);
        }
    }
}