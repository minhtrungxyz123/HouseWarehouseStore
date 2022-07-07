namespace HouseWarehouseStore.Data.Entities
{
    public partial class ProductCategory
    {
        public string ProductCategorieId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string CoverImage { get; set; } = null!;
        public string? Url { get; set; }
        public int Soft { get; set; }
        public bool Active { get; set; }
        public bool Home { get; set; }
        public string? ParentId { get; set; }
        public string? TitleMeta { get; set; }
        public string? DescriptionMeta { get; set; }
        public string? Body { get; set; }
        public string? Icon { get; set; }
    }
}