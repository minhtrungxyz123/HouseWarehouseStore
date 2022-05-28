using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseWarehouseStore.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("Product");

            entity.HasIndex(e => e.CollectionId, "IX_Products_CollectionID");

            entity.HasIndex(e => e.ProductCategorieId, "IX_Products_ProductCategorieID");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.Property(e => e.BarCode).HasMaxLength(50);

            entity.Property(e => e.CollectionId).HasColumnName("CollectionID");

            entity.Property(e => e.CreateBy).HasDefaultValueSql("(N'admin')");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Description).IsRequired();

            entity.Property(e => e.Factory).HasMaxLength(501);

            entity.Property(e => e.Image).IsRequired();

            entity.Property(e => e.Name).IsRequired();

            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.ProductCategorieId).HasColumnName("ProductCategorieID");

            entity.Property(e => e.QuyCach).HasMaxLength(500);

            entity.Property(e => e.SaleOff).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.Sort).HasDefaultValueSql("((1))");

            entity.Property(e => e.TitleMeta).HasMaxLength(100);

            entity.HasOne(d => d.Collection)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.CollectionId);

            entity.HasOne(d => d.ProductCategorie)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategorieId);

            entity.HasMany(d => d.Tags)
                .WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "TagProduct",
                    l => l.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
                    r => r.HasOne<Product>().WithMany().HasForeignKey("ProductId"),
                    j =>
                    {
                        j.HasKey("ProductId", "TagId");

                        j.ToTable("TagProducts");

                        j.HasIndex(new[] { "TagId" }, "IX_TagProducts_TagID");

                        j.IndexerProperty<int>("ProductId").HasColumnName("ProductID");

                        j.IndexerProperty<int>("TagId").HasColumnName("TagID");
                    });
        }
    }
}