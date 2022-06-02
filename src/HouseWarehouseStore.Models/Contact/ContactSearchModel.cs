using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class ContactSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}