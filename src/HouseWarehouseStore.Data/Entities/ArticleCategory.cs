namespace HouseWarehouseStore.Data.Entities
{
    public partial class ArticleCategory
    {
        public ArticleCategory()
        {
            Articles = new HashSet<Article>();
        }

        public int ArticleCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Url { get; set; }
        public int CategorySort { get; set; }
        public bool CategoryActive { get; set; }
        public int? ParentId { get; set; }
        public bool ShowHome { get; set; }
        public bool ShowMenu { get; set; }
        public string Slug { get; set; }
        public bool Hot { get; set; }
        public string TitleMeta { get; set; }
        public string DescriptionMeta { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}