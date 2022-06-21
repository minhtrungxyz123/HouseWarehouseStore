using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class TagProductSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}