using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> entity)
        {
            entity.ToTable("Members");

            entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false);

            entity.Property(e => e.Address).HasMaxLength(200);

            entity.Property(e => e.ConfirmEmail)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");

            entity.Property(e => e.Email).HasMaxLength(50);

            entity.Property(e => e.Fullname).HasMaxLength(50);

            entity.Property(e => e.HomePage).HasMaxLength(200);

            entity.Property(e => e.LockAccount)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");

            entity.Property(e => e.Role).HasDefaultValueSql("(N'')");

            entity.Property(e => e.Token).HasColumnName("token");
        }
    }
}