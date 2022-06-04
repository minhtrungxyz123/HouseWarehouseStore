using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> entity)
        {
            entity.ToTable("Albums");

            entity.Property(e => e.AlbumId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("AlbumID");

            entity.Property(e => e.Description).HasMaxLength(500);

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.Property(e => e.Title).HasMaxLength(100);
        }
    }
}