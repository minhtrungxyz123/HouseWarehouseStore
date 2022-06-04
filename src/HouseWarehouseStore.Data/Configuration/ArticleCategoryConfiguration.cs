using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class ArticleCategoryConfiguration : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> entity)
        {
            entity.ToTable("ArticleCategory");

            entity.Property(e => e.ArticleCategoryId)
                    .HasMaxLength(36)
                    .IsUnicode(false);

            entity.Property(e => e.CategoryName).HasMaxLength(50);

            entity.Property(e => e.ParentId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.Slug).HasMaxLength(100);

            entity.Property(e => e.TitleMeta).HasMaxLength(100);

            entity.Property(e => e.Url).HasMaxLength(500);
        }
    }
}