namespace HouseWarehouseStore.Data.Entities
{
    public partial class Size
    {
        public Size()
        {
            ProductSizeColors = new HashSet<ProductSizeColor>();
        }

        public int SizeId { get; set; }
        public string SizeProduct { get; set; }

        public virtual ICollection<ProductSizeColor> ProductSizeColors { get; set; }
    }
}