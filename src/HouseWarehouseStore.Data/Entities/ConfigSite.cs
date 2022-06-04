using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class ConfigSite
    {
        public string ConfigSiteId { get; set; } = null!;
        public string? Facebook { get; set; }
        public string? GooglePlus { get; set; }
        public string? Youtube { get; set; }
        public string? Linkedin { get; set; }
        public string? Twitter { get; set; }
        public string? GoogleAnalytics { get; set; }
        public string? LiveChat { get; set; }
        public string? GoogleMap { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ContactInfo { get; set; }
        public string? FooterInfo { get; set; }
        public string? Hotline { get; set; }
        public string? Email { get; set; }
        public string? CoverImage { get; set; }
        public string? SaleOffProgram { get; set; }
        public string? NameShopee { get; set; }
        public string? UrlWeb { get; set; }
        public string? FbMessage { get; set; }
    }
}
