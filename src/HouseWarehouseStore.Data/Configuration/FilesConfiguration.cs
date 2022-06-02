using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class FilesConfiguration : IEntityTypeConfiguration<Files>
    {
        public void Configure(EntityTypeBuilder<Files> entity)
        {
            entity.ToTable("Files");

            entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            entity.Property(e => e.FileName).IsRequired().HasMaxLength(255);

            entity.Property(e => e.Path).HasMaxLength(500);

            entity.Property(e => e.Size).IsRequired().HasColumnType("decimal(15, 2)");

            entity.Property(e => e.Extension).HasMaxLength(50);

            entity.Property(e => e.MimeType).HasMaxLength(255);

            entity.Property(e => e.CollectionId).HasColumnName("CollectionId");
        }
    }
}