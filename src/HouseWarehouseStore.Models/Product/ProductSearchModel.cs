using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class ProductSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
        public string? Price { get; set; }
    }
}