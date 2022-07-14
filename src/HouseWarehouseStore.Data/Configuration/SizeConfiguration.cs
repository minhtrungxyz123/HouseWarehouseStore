using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> entity)
        {
            entity.ToTable("Sizes");

            entity.Property(e => e.SizeId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("SizeID");

            entity.Property(e => e.SizeProduct).HasMaxLength(50);

            entity.Property(e => e.ProductId)
                .HasMaxLength(36)
                .IsUnicode(false);
        }
    }
}