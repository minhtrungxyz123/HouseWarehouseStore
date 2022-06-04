using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class ConfigSiteConfiguration : IEntityTypeConfiguration<ConfigSite>
    {
        public void Configure(EntityTypeBuilder<ConfigSite> entity)
        {
            entity.ToTable("ConfigSite");

            entity.Property(e => e.ConfigSiteId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("ConfigSiteID");

            entity.Property(e => e.CoverImage).HasMaxLength(500);

            entity.Property(e => e.Description).HasMaxLength(500);

            entity.Property(e => e.Email).HasMaxLength(100);

            entity.Property(e => e.Facebook).HasMaxLength(500);

            entity.Property(e => e.GoogleAnalytics).HasMaxLength(4000);

            entity.Property(e => e.GoogleMap).HasMaxLength(4000);

            entity.Property(e => e.GooglePlus).HasMaxLength(500);

            entity.Property(e => e.Hotline).HasMaxLength(50);

            entity.Property(e => e.Linkedin).HasMaxLength(500);

            entity.Property(e => e.LiveChat).HasMaxLength(4000);

            entity.Property(e => e.NameShopee)
                .HasMaxLength(100)
                .HasColumnName("nameShopee");

            entity.Property(e => e.Title).HasMaxLength(200);

            entity.Property(e => e.Twitter).HasMaxLength(500);

            entity.Property(e => e.UrlWeb)
                .HasMaxLength(100)
                .HasColumnName("urlWeb");

            entity.Property(e => e.Youtube).HasMaxLength(500);
        }
    }
}