using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> entity)
        {
            entity.ToTable("Contacts");

            entity.Property(e => e.ContactId)
                   .HasMaxLength(36)
                   .IsUnicode(false)
                   .HasColumnName("ContactID");

            entity.Property(e => e.Address).HasMaxLength(300);

            entity.Property(e => e.Body).HasMaxLength(4000);

            entity.Property(e => e.Fullname).HasMaxLength(50);

            entity.Property(e => e.Subject).HasMaxLength(100);
        }
    }
}