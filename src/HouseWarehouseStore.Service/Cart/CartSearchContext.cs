namespace HouseWarehouseStore.Service
{
    public class CartSearchContext
    {
        public string? Keyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? MemberId { get; set; }
        public string? ProductId { get; set; }
    }
}