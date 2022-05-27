using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class AboutConfiguration : IEntityTypeConfiguration<About>
    {
        public void Configure(EntityTypeBuilder<About> entity)
        {
            entity.ToTable("About");

            entity.Property(e => e.AboutId).HasColumnName("AboutID");

            entity.Property(e => e.CoverImage).HasMaxLength(500);

            entity.Property(e => e.Image).HasMaxLength(500);

            entity.Property(e => e.Subject)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}