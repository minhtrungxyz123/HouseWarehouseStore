namespace HouseWarehouseStore.Models
{
    public class ArticleCategoryModel
    {
        public string? ArticleCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Url { get; set; }
        public int CategorySort { get; set; }
        public bool CategoryActive { get; set; }
        public string? ParentId { get; set; }
        public bool ShowHome { get; set; }
        public bool ShowMenu { get; set; }
        public string Slug { get; set; }
        public bool Hot { get; set; }
        public string TitleMeta { get; set; }
        public string DescriptionMeta { get; set; }
    }
}