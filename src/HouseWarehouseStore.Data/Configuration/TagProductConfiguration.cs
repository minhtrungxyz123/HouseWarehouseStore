using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class TagProductConfiguration : IEntityTypeConfiguration<TagProduct>
    {
        public void Configure(EntityTypeBuilder<TagProduct> entity)
        {
            entity.ToTable("TagProduct");

            entity.HasKey(e => new { e.ProductId, e.TagId });

            entity.HasIndex(e => e.TagId, "IX_TagProducts_TagID");

            entity.Property(e => e.ProductId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("ProductID");

            entity.Property(e => e.TagId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("TagID");
        }
    }
}