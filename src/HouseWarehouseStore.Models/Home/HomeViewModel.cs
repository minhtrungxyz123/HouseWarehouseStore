using HouseWarehouseStore.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseWarehouseStore.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Banner> Banners { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<ViewProducts> Products { get; set; }
        public IEnumerable<Album> Albums { get; set; }
        public IEnumerable<ConfigSite> ConfigSites { get; set; }
        public IEnumerable<ViewProducts> ViewProducts { get; set; }
        public IEnumerable<ItemBoxProductHome> ItemBoxProductHomes { get; set; }

        public class ItemBoxProductHome
        {
            public ProductCategory ProductCategory { get; set; }
            public IEnumerable<Product> Products { get; set; }
        }
    }

    public class GetSizeId
    {
        public string SizeProduc { get; set; }
    }

    public class GetColorId
    {
        public string NameColor { get; set; }
        public string Code { get; set; }
    }

    public class ProductCategoryListSP
    {
        public string Name { get; set; }
        public string ProductCategorieID { get; set; }
        public string Image { get; set; }
    }

    public class ViewProducts
    {
        public DateTime CreateDate { get; set; }
        public string Image { get; set; }
        public string ProductID { get; set; }
        public string Name { get; set; }
        public decimal SaleOff { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }

        public bool Hot { get; set; }
        public int Sort { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public ProductLike ProductLike { get; set; }
        public IEnumerable<ViewProducts> Products { get; set; }
        public IEnumerable<Product> ViewProducts { get; set; }
        public ProductCategory RootCategory { get; set; }
        public Collection Collection { get; set; }
        public IEnumerable<TagProduct> TagProducts { get; set; }
        public IEnumerable<ProductSizeColor> ProductSizeColors { get; set; }
        public IEnumerable<Banner> Banners { get; set; }
        public IEnumerable<GetColorId> GetColors { get; set; }
        public IEnumerable<GetSizeId> GetSizes { get; set; }
    }

    public class ProductSCViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ProductSizeColor> ProductSizeColors { get; set; }
    }

    public class ReviewViewModel
    {
        public Article Article { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<ArticleCategory> ListArticles { get; set; }
    }
}