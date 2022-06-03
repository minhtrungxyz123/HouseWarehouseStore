using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class CollectionConfiguration : IEntityTypeConfiguration<Collection>
    {
        public void Configure(EntityTypeBuilder<Collection> entity)
        {
            entity.ToTable("Collections");

            entity.Property(e => e.CollectionId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            entity.Property(e => e.BarCode).HasMaxLength(50);

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Factory).HasMaxLength(500);

            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.TitleMeta).HasMaxLength(100);
        }
    }
}