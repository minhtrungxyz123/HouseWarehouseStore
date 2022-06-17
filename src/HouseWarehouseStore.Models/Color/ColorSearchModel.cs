using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class ColorSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}