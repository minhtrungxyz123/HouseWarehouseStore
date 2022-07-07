using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> entity)
        {
            entity.ToTable("Comments");

            entity.Property(e => e.CommentId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("CommentId");

            entity.Property(e => e.CustomerName).HasMaxLength(500);

            entity.Property(e => e.Profession).HasMaxLength(500);

            entity.Property(e => e.Star).HasMaxLength(500);

            entity.Property(e => e.Image).HasMaxLength(500);

            entity.Property(e => e.Contents).HasMaxLength(500);

            entity.Property(e => e.ProductId)
                .HasMaxLength(36)
                .IsUnicode(false);
        }
    }
}