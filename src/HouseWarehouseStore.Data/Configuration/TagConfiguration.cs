using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> entity)
        {
            entity.ToTable("Tags");

            entity.Property(e => e.TagId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("TagID");

            entity.Property(e => e.Name).HasMaxLength(100);
        }
    }
}