using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class BannerConfiguration : IEntityTypeConfiguration<Banner>
    {
        public void Configure(EntityTypeBuilder<Banner> entity)
        {
            entity.ToTable("Banner");

            entity.Property(e => e.BannerId).HasColumnName("BannerID");

            entity.Property(e => e.BannerName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.CoverImage).HasMaxLength(500);

            entity.Property(e => e.Title).HasMaxLength(100);

            entity.Property(e => e.Url).HasMaxLength(500);
        }
    }
}