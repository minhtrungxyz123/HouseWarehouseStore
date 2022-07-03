using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> entity)
        {
            entity.ToTable("Notifications");

            entity.Property(e => e.NotiId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("NotiId");

            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.NotiHeader).HasMaxLength(500);

            entity.Property(e => e.NotiBody).HasMaxLength(500);

            entity.Property(e => e.Url).HasMaxLength(500);

            entity.Property(e => e.CreatedDate).IsRequired();

            entity.Property(e => e.IsRead).IsRequired();
        }
    }
}