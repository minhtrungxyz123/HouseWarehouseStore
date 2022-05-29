using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class AggregatedCounterSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}