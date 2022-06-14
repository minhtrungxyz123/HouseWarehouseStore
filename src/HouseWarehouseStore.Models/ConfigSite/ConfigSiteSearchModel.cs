using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class ConfigSiteSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}