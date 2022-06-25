using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> entity)
        {
            entity.ToTable("Admins");

            entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false);

            entity.Property(e => e.Password).IsRequired();

            entity.Property(e => e.Role).IsRequired();

            entity.Property(e => e.Username).IsRequired();

            entity.Property(e => e.Address).IsRequired();

            entity.Property(e => e.Image).IsRequired();

            entity.Property(e => e.FullName).IsRequired();

            entity.Property(e => e.CreateDate).IsRequired();

            entity.Property(e => e.Sex).IsRequired();

            entity.Property(e => e.Position).IsRequired();

            entity.Property(e => e.Email).IsRequired();
        }
    }
}