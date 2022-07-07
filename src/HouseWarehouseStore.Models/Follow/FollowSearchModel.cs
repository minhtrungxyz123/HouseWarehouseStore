using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class FollowSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}