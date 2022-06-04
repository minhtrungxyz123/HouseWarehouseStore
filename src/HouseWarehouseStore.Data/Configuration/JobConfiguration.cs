using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> entity)
        {
            entity.ToTable("Job");

            entity.ToTable("Job", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Job_ExpireAt")
                .HasFilter("([ExpireAt] IS NOT NULL)");

            entity.HasIndex(e => e.StateName, "IX_HangFire_Job_StateName")
                .HasFilter("([StateName] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.Property(e => e.ExpireAt).HasColumnType("datetime");

            entity.Property(e => e.StateName).HasMaxLength(20);
        }
    }
}