namespace HouseWarehouseStore.Service
{
    public class ProductLikeSearchContext
    {
        public string? Keyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? IdMember { get; set; }
    }
}