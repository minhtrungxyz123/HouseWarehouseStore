using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class SchemaConfiguration : IEntityTypeConfiguration<Schema>
    {
        public void Configure(EntityTypeBuilder<Schema> entity)
        {
            entity.ToTable("Schema");

            entity.HasKey(e => e.Version)
                     .HasName("PK_HangFire_Schema");

            entity.ToTable("Schema", "HangFire");

            entity.Property(e => e.Version).ValueGeneratedNever();
        }
    }
}