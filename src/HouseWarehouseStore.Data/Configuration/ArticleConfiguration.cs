using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> entity)
        {
            entity.ToTable("Articles");

            entity.HasIndex(e => e.ArticleCategoryId, "IX_Articles_ArticleCategoryId");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.ArticleCategoryId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.Description).HasMaxLength(500);

            entity.Property(e => e.DescriptionMetaTitle).HasMaxLength(500);

            entity.Property(e => e.KeyWord).HasMaxLength(500);

            entity.Property(e => e.Subject).HasMaxLength(100);

            entity.Property(e => e.TitleMeta).HasMaxLength(100);

            entity.Property(e => e.Url).HasMaxLength(300);

        }
    }
}