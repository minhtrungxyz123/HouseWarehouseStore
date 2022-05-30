namespace HouseWarehouseStore.Models
{
    public class ArticleModel
    {
        public int? Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public DateTime CreateDate { get; set; }
        public int View { get; set; }
        public int ArticleCategoryId { get; set; }
        public bool Active { get; set; }
        public bool Hot { get; set; }
        public bool Home { get; set; }
        public string Url { get; set; }
        public string TitleMeta { get; set; }
        public string KeyWord { get; set; }
        public string DescriptionMetaTitle { get; set; }
    }
}