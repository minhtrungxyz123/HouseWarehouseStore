namespace HouseWarehouseStore.Service
{
    public class CategorySearchContext
    {
        public string? Keyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public  string? CategoryId { get; set; }
    }
}