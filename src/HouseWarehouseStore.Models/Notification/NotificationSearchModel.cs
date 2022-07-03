using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class NotificationSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}