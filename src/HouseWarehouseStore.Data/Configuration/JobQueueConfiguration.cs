using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class JobQueueConfiguration : IEntityTypeConfiguration<JobQueue>
    {
        public void Configure(EntityTypeBuilder<JobQueue> entity)
        {
            entity.ToTable("JobQueue");

            entity.HasKey(e => new { e.Queue, e.Id })
                    .HasName("PK_HangFire_JobQueue");

            entity.ToTable("JobQueue", "HangFire");

            entity.Property(e => e.Queue).HasMaxLength(50);

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.FetchedAt).HasColumnType("datetime");
        }
    }
}