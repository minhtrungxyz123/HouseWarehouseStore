using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class JobParameterConfiguration : IEntityTypeConfiguration<JobParameter>
    {
        public void Configure(EntityTypeBuilder<JobParameter> entity)
        {
            entity.ToTable("JobParameter");

            entity.HasKey(e => new { e.JobId, e.Name })
                    .HasName("PK_HangFire_JobParameter");

            entity.ToTable("JobParameter", "HangFire");

            entity.Property(e => e.Name).HasMaxLength(40);
        }
    }
}