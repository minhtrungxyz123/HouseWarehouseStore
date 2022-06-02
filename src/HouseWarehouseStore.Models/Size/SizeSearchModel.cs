using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class SizeSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}