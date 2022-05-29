using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class AdminSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}