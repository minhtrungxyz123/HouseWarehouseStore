using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class BannerConfiguration : IEntityTypeConfiguration<Banner>
    {
        public void Configure(EntityTypeBuilder<Banner> entity)
        {
            entity.ToTable("Banners");

            entity.Property(e => e.BannerId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("BannerID");

            entity.Property(e => e.BannerName).HasMaxLength(100);

            entity.Property(e => e.CoverImage).HasMaxLength(500);

            entity.Property(e => e.GroupId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.Title).HasMaxLength(100);

            entity.Property(e => e.Url).HasMaxLength(500);
        }
    }
}