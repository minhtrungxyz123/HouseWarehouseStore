namespace HouseWarehouseStore.Models
{
    public class ProductLikeModel
    {
        public int? ProductLikeId { get; set; }
        public int ProductId { get; set; }
        public int MemberId { get; set; }
        public int? ProductsProductId { get; set; }
    }
}