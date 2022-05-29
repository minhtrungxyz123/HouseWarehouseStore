namespace HouseWarehouseStore.Models
{
    public class ProductSizeColorModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int? ProductsProductId { get; set; }
    }
}