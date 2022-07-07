using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    internal class CommentSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}