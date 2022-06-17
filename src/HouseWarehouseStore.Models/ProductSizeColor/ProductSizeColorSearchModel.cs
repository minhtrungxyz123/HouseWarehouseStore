using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class ProductSizeColorSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}