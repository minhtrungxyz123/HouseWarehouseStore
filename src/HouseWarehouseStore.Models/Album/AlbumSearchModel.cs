using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class AlbumSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}