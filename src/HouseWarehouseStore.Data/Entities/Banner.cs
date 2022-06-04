using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class Banner
    {
        public string BannerId { get; set; } = null!;
        public string BannerName { get; set; } = null!;
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Active { get; set; }
        public string GroupId { get; set; } = null!;
        public string? Url { get; set; }
        public int Soft { get; set; }
        public string? CoverImage { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}
