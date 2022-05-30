using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class MemberSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}