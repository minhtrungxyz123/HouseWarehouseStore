using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> entity)
        {
            entity.ToTable("State");

            entity.HasKey(e => new { e.JobId, e.Id })
                    .HasName("PK_HangFire_State");

            entity.ToTable("State", "HangFire");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.Reason).HasMaxLength(100);

            entity.HasOne(d => d.Job)
                .WithMany(p => p.States)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK_HangFire_State_Job");
        }
    }
}