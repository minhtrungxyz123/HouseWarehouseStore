using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class Article
    {
        public string Id { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Body { get; set; }
        public string? Image { get; set; }
        public DateTime CreateDate { get; set; }
        public int View { get; set; }
        public string ArticleCategoryId { get; set; } = null!;
        public bool Active { get; set; }
        public bool Hot { get; set; }
        public bool Home { get; set; }
        public string? Url { get; set; }
        public string? TitleMeta { get; set; }
        public string? KeyWord { get; set; }
        public string? DescriptionMetaTitle { get; set; }
    }
}
