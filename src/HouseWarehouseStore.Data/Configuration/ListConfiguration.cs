using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class ListConfiguration : IEntityTypeConfiguration<List>
    {
        public void Configure(EntityTypeBuilder<List> entity)
        {
            entity.ToTable("List");

            entity.HasKey(e => new { e.Key, e.Id })
                    .HasName("PK_HangFire_List");

            entity.ToTable("List", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_List_ExpireAt")
                .HasFilter("([ExpireAt] IS NOT NULL)");

            entity.Property(e => e.Key).HasMaxLength(100);

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
        }
    }
}