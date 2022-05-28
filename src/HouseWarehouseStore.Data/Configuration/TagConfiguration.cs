using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> entity)
        {
            entity.ToTable("Tag");

            entity.Property(e => e.TagId).HasColumnName("TagID");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}