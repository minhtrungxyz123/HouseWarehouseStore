using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class FileSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}