namespace Master.Service
{
    public class ProductSizeColorSearchContext
    {
        public string? Keyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public  string? SizeId { get; set; }
        public  string? ColorId { get; set; }
        public string? ProductId { get; set; }
    }
}