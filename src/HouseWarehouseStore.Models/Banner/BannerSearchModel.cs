using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class BannerSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}