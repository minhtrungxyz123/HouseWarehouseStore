using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> entity)
        {
            entity.ToTable(" Member");

            entity.Property(e => e.Address).HasMaxLength(200);

            entity.Property(e => e.ConfirmEmail)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Fullname)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.HomePage).HasMaxLength(200);

            entity.Property(e => e.LockAccount)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");

            entity.Property(e => e.Password).IsRequired();

            entity.Property(e => e.Role)
                .IsRequired()
                .HasDefaultValueSql("(N'')");

            entity.Property(e => e.Token).HasColumnName("token");
        }
    }
}