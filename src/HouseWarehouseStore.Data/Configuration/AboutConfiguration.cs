using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class AboutConfiguration : IEntityTypeConfiguration<About>
    {
        public void Configure(EntityTypeBuilder<About> entity)
        {
            entity.ToTable("Abouts");

            entity.Property(e => e.AboutId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("AboutID");

            entity.Property(e => e.CoverImage).HasMaxLength(500);

            entity.Property(e => e.Image).HasMaxLength(500);

            entity.Property(e => e.Subject).HasMaxLength(100);
        }
    }
}