namespace HouseWarehouseStore.Models
{
    public class ProductSizeColorModel
    {
        public string? Id { get; set; }
        public string ProductId { get; set; }
        public string ColorId { get; set; }
        public string SizeId { get; set; }
        public string? ProductsProductId { get; set; }
    }
}