using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = HouseWarehouseStore.Data.Entities.File;

namespace HouseWarehouseStore.Data.Configuration
{
    public class FilesConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> entity)
        {
            entity.ToTable("Files");

            entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false);

            entity.Property(e => e.CollectionId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.Extension)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.MimeType)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Path)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Size).HasColumnType("decimal(15, 2)");
        }
    }
}