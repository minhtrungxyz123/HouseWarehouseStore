using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> entity)
        {
            entity.ToTable("Contact");

            entity.Property(e => e.ContactId).HasColumnName("ContactID");

            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(e => e.Body)
                .IsRequired()
                .HasMaxLength(4000);

            entity.Property(e => e.Email).IsRequired();

            entity.Property(e => e.Fullname)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Subject)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}