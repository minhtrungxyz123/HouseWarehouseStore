namespace HouseWarehouseStore.Data.Entities
{
    public partial class ProductSizeColor
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int? ProductsProductId { get; set; }

        public virtual Color Color { get; set; }
        public virtual Product ProductsProduct { get; set; }
        public virtual Size Size { get; set; }
    }
}