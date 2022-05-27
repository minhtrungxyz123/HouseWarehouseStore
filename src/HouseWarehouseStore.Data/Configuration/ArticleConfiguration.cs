using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> entity)
        {
            entity.ToTable("Article");

            entity.HasIndex(e => e.ArticleCategoryId, "IX_Articles_ArticleCategoryId");

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(e => e.DescriptionMetaTitle).HasMaxLength(500);

            entity.Property(e => e.KeyWord).HasMaxLength(500);

            entity.Property(e => e.Subject)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.TitleMeta).HasMaxLength(100);

            entity.Property(e => e.Url).HasMaxLength(300);

            entity.HasOne(d => d.ArticleCategory)
                .WithMany(p => p.Articles)
                .HasForeignKey(d => d.ArticleCategoryId);
        }
    }
}