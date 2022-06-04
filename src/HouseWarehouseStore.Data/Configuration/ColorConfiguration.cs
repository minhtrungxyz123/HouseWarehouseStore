using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> entity)
        {
            entity.ToTable("Color");

            entity.Property(e => e.ColorId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("ColorID");
        }
    }
}