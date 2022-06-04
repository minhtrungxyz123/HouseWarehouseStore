namespace HouseWarehouseStore.Models
{
    public class ProductCategoryModel
    {
        public string? ProductCategorieId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string CoverImage { get; set; }
        public string Url { get; set; }
        public int Soft { get; set; }
        public bool Active { get; set; }
        public bool Home { get; set; }
        public string? ParentId { get; set; }
        public string TitleMeta { get; set; }
        public string DescriptionMeta { get; set; }
        public string Body { get; set; }
    }
}