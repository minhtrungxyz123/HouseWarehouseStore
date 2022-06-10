using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class ArticleCategorySearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}