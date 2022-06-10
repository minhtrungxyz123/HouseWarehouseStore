using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class ArticleSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}