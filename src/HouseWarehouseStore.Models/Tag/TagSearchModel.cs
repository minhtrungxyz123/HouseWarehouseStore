using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class TagSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}