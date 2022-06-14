using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class VoucherSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}