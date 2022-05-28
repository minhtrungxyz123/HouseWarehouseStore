using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> entity)
        {
            entity.ToTable("Size");

            entity.Property(e => e.SizeId).HasColumnName("SizeID");

            entity.Property(e => e.SizeProduct).HasMaxLength(50);
        }
    }
}