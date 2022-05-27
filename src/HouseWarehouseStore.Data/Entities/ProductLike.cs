namespace HouseWarehouseStore.Data.Entities
{
    public partial class ProductLike
    {
        public int ProductLikeId { get; set; }
        public int ProductId { get; set; }
        public int MemberId { get; set; }
        public int? ProductsProductId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Product ProductsProduct { get; set; }
    }
}