using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class CollectionSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}