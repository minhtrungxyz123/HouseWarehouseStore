using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class CommentSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}